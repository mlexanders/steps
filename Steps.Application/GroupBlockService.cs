using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
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
            include: gb => gb.Include(gb => gb.PreBlocks)
                .ThenInclude(pb => pb.AthleteBlocks),
            trackingType: TrackingType.NoTracking);

        return groupBlocks?.ToList() ?? new List<GroupBlock>();
    }

    /// <summary>
    /// Помечает спортсмена как подтвержденного в заданном блоке.
    /// </summary>
    public async Task MarkAthlete(GroupBlock groupBlock, Guid athleteId)
    {
        var athleteBlockRepository = _unitOfWork.GetRepository<ConfirmationAthleteBlock>();

        var athleteBlock = await athleteBlockRepository.GetFirstOrDefaultAsync(
                               predicate: b => b.BlockId.Equals(groupBlock.Id) && b.AthleteId.Equals(athleteId),
                               trackingType: TrackingType.NoTracking)
                           ?? throw new StepsBusinessException("Спортсмен в заданном блоке не найден.");

        athleteBlock.IsConfirmed = true;
    }


    /// <summary>
    /// Генерирует групповые блоки для соревнования.
    /// </summary>
    public async Task GenerateGroupBlocks(Contest contest, int athletesPerGroup)
    {
        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        var entryRepository = _unitOfWork.GetRepository<Entry>();

        var groupBlocksExist = await groupBlockRepository.ExistsAsync(gb => gb.ContestId == contest.Id);
        if (groupBlocksExist)
        {
            throw new StepsBusinessException("Список уже сформирован.");
        }

        var athleteEntries = await entryRepository.GetAllAsync(
            predicate: entry => entry.ContestId == contest.Id && entry.IsSuccess,
            selector: entry => entry.Athletes.Select(a => a.Id).Distinct(),
            trackingType: TrackingType.NoTracking);

        var athleteIds = athleteEntries.SelectMany(a => a).Distinct().ToList();
        var sortedAthleteIds = GetSortedAthletes(athleteIds).ToList();

        var judgeCount = contest.Judjes?.Count ?? DefaultJudgesCount;
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

                var preBlock = new PreBlock
                {
                    GroupBlock = groupBlock,
                    ExitTime = exitTime.ToUniversalTime(),
                    AthleteBlocks = []
                };
                groupBlock.PreBlocks.Add(preBlock);

                var sequenceNumber = 1;
                foreach (var athleteId in athleteSubGroups[index])
                {
                    preBlock.AthleteBlocks.Add(new ConfirmationAthleteBlock
                    {
                        SequenceNumber = sequenceNumber,
                        AthleteId = athleteId,
                        Block = preBlock,
                        IsConfirmed = false
                    });
                    sequenceNumber++;
                }
            }

            groupBlock.EndTime = groupBlock.PreBlocks.Last().ExitTime;
        }

        await groupBlockRepository.InsertAsync(groupBlocks);
        await _unitOfWork.SaveChangesAsync();
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