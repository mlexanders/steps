using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Domain.Entities.GroupBlocks.SubGroups;
using Steps.Shared.Exceptions;

namespace Steps.Application;

public class GroupBlockService
{
    private const int DefaultJudgesCount = 2;
    private static readonly TimeSpan GroupBlockInterval = TimeSpan.FromMinutes(30);
    private static readonly TimeSpan AthleteExitInterval = TimeSpan.FromMinutes(2);

    private readonly IUnitOfWork _unitOfWork;

    public GroupBlockService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Возвращает список групповых блоков для соревнования.
    /// </summary>
    public async Task<List<GroupBlock>> GetGroupBlocks(Contest contest)
    {
        //TODO: переделать под ViewModels

        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();

        var groupBlocks = await groupBlockRepository.GetAllAsync(
            predicate: gb => gb.ContestId.Equals(contest.Id),
            include: gb => gb.Include(a => a.PreSubGroups)
                .ThenInclude(pb => pb.AthleteBlocks),
            trackingType: TrackingType.NoTracking);

        return groupBlocks?.ToList() ?? new List<GroupBlock>();
    }

    /// <summary>
    /// Помечает спортсмена как подтвержденного в заданном блоке.
    /// </summary>
    public async Task MarkAthlete(GroupBlock groupBlock, Guid athleteId)
    {
        var athleteBlockRepository = _unitOfWork.GetRepository<ConfirmationAthleteSubGroup>();

        var athleteBlock = await athleteBlockRepository.GetFirstOrDefaultAsync(
                               predicate: b => b.SubGroupId.Equals(groupBlock.Id) && b.AthleteId.Equals(athleteId),
                               trackingType: TrackingType.Tracking)
                           ?? throw new StepsBusinessException("Спортсмен в этом предварительном блоке не найден.");

        athleteBlock.IsConfirmed = true;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task GenerateFinalGroupBlocks(Guid groupBlockId)
    {
        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
                             predicate: gb => gb.Id.Equals(groupBlockId),
                             include: gb => gb
                                 .Include(a => a.Contest)
                                 .Include(a => a.PreSubGroups)
                                 .ThenInclude(ah => ah.AthleteBlocks),
                             trackingType: TrackingType.Tracking)
                         ?? throw new StepsBusinessException("Предварительный блок не найден");

        ValidateContestStatus(groupBlock.Contest);
        
        var isExistFinalGroups = groupBlock.PreSubGroups.Any(s => s.FinalSubGroupId != null);
        if (isExistFinalGroups)
            throw new StepsBusinessException("Финальный блок для выбранного предварительного блока уже сформирован");

        var athleteSubGroups = groupBlock.PreSubGroups
            .Where(p => p.AthleteBlocks.Any(b => b.IsConfirmed))
            .Select(p => p).ToList();

        if (athleteSubGroups is null || athleteSubGroups.Count == 0)
            throw new StepsBusinessException("Предварительный групповой блок пуст");

        for (var index = 0; index < athleteSubGroups.Count; index++)
        {
            var preSubGroup = athleteSubGroups[index];
            var exitTime = groupBlock.StartTime.Add(AthleteExitInterval * (index + 1));

            var finalSubGroup = new FinalSubGroup()
            {
                GroupBlock = groupBlock,
                ExitTime = exitTime.ToUniversalTime(),
                AthleteBlocks = []
            };

            preSubGroup.FinalSubGroup = finalSubGroup;
            groupBlock.FinalSubGroups.Add(finalSubGroup);

            var sequenceNumber = 1;
            foreach (var athleteId in preSubGroup.AthleteBlocks)
            {
                finalSubGroup.AthleteBlocks.Add(new FinalAthleteSubGroup()
                {
                    SequenceNumber = sequenceNumber,
                    AthleteId = athleteId.AthleteId,
                    SubGroup = finalSubGroup,
                });
                sequenceNumber++;
            }
        }

        groupBlockRepository.Update(groupBlock);
        await _unitOfWork.SaveChangesAsync();
    }


    /// <summary>
    /// Генерирует ПРЕДВАРИТЕЛЬНЫЕ групповые блоки для соревнования.
    /// </summary>
    public async Task GeneratePreGroupBlocks(Contest contest, int athletesPerGroup)
    {
        ValidateContestStatus(contest);

        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        var entryRepository = _unitOfWork.GetRepository<Entry>();

        var groupBlocksExist = await groupBlockRepository.ExistsAsync(gb => gb.ContestId.Equals(contest.Id));
        if (groupBlocksExist)
            throw new StepsBusinessException("Список уже сформирован.");

        var athleteEntries = await entryRepository.GetAllAsync(
            predicate: entry => entry.ContestId.Equals(contest.Id) && entry.IsSuccess,
            selector: entry => entry.Athletes.Select(a => a.Id).Distinct(),
            trackingType: TrackingType.NoTracking);

        var athleteIds = athleteEntries.SelectMany(a => a).Distinct().ToList();
        var sortedAthleteIds = GetSortedAthletes(athleteIds).ToList();

        var judgeCount = contest.Judges?.Count ?? DefaultJudgesCount;
        if (judgeCount == 0) judgeCount = DefaultJudgesCount;

        var groupedAthletes = SplitIntoBatches(sortedAthleteIds, athletesPerGroup);
        var groupBlocks = new List<GroupBlock>();

        foreach (var athleteBatch in groupedAthletes)
        {
            var startTime = groupBlocks.Count > 0
                ? groupBlocks.Last().EndTime.Add(GroupBlockInterval)
                : contest.StartDate;

            var groupBlock = new GroupBlock
            {
                ContestId = contest.Id,
                StartTime = startTime.ToUniversalTime()
            };
            groupBlocks.Add(groupBlock);

            var athleteSubGroups = SplitIntoBatches(athleteBatch, judgeCount);

            for (var index = 0; index < athleteSubGroups.Count; index++)
            {
                var exitTime = groupBlock.StartTime.Add(AthleteExitInterval * (index + 1));

                var preSubGroup = new PreSubGroup
                {
                    GroupBlock = groupBlock,
                    ExitTime = exitTime.ToUniversalTime(),
                    AthleteBlocks = []
                };
                groupBlock.PreSubGroups.Add(preSubGroup);

                var sequenceNumber = 1;
                foreach (var athleteId in athleteSubGroups[index])
                {
                    preSubGroup.AthleteBlocks.Add(new ConfirmationAthleteSubGroup
                    {
                        SequenceNumber = sequenceNumber,
                        AthleteId = athleteId,
                        SubGroup = preSubGroup,
                        IsConfirmed = false
                    });
                    sequenceNumber++;
                }
            }

            groupBlock.EndTime = groupBlock.PreSubGroups.Last().ExitTime;
        }

        await groupBlockRepository.InsertAsync(groupBlocks);
        await _unitOfWork.SaveChangesAsync();
    }
    
    /// <summary>
    /// Валидация состояния соревнования.
    /// </summary>
    /// <param name="contest"></param>
    /// <exception cref="StepsBusinessException"></exception>
    private static void ValidateContestStatus(Contest contest)
    {
        if (contest.Status is ContestStatus.Open)
            throw new StepsBusinessException("Сбор заявок не закрыт.");
        if (contest.Status is ContestStatus.Finished)
            throw new StepsBusinessException("Мероприятие уже завершено.");
    }


    /// <summary>
    /// Разделяет список на подгруппы заданного размера.
    /// </summary>
    private static List<List<T>> SplitIntoBatches<T>(List<T> items, int batchSize)
    {
        return items
            .Select((item, index) => new { item, index })
            .GroupBy(x => x.index / batchSize)
            .Select(g => g.Select(x => x.item).ToList())
            .ToList();
    }

    /// <summary>
    /// Сортирует список спортсменов (при необходимости можно добавить логику сортировки).
    /// </summary>
    private static IEnumerable<Guid> GetSortedAthletes(IEnumerable<Guid> athleteIds)
    {
        return athleteIds; // TODO: 
    }
}