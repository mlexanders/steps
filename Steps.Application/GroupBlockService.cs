using Calabonga.UnitOfWork;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules;
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
    /// Помечает спортсмена как подтвержденного в заданном блоке.
    /// </summary>
    public async Task MarkAthlete(GroupBlock groupBlock, Guid athleteId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Генерирует ПРЕДВАРИТЕЛЬНЫЕ групповые блоки для соревнования.
    /// </summary>
    public async Task GenerateGroupBlocks(Contest contest, int athletesPerGroup)
    {
        ValidateContestStatus(contest);

        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        var entryRepository = _unitOfWork.GetRepository<Entry>();

        var groupBlocksExist = await groupBlockRepository.ExistsAsync(gb => gb.ContestId.Equals(contest.Id));
        if (groupBlocksExist)
            throw new StepsBusinessException("Блоки уже сформированы.");

        var athleteEntries = await entryRepository.GetAllAsync(
            predicate: entry => entry.ContestId.Equals(contest.Id) && entry.IsSuccess,
            selector: entry => entry.Athletes.Select(a => a.Id).Distinct(),
            trackingType: TrackingType.NoTracking);

        var athleteIds = athleteEntries.SelectMany(a => a).Distinct().ToList();
        var sortedAthleteIds = GetSortedAthletes(athleteIds).ToList();

        var judgeCount = contest.Judges?.Count ?? DefaultJudgesCount;
        if (judgeCount == 0) judgeCount = DefaultJudgesCount;

        var athleteByGroupBlock = SplitIntoBatches(sortedAthleteIds, athletesPerGroup);

        var groupBlocks = CreateGroupBlocks(contest, athleteByGroupBlock, judgeCount);

        await groupBlockRepository.InsertAsync(groupBlocks);
        await _unitOfWork.SaveChangesAsync();
    }

    private static List<GroupBlock> CreateGroupBlocks(Contest contest, List<List<Guid>> athleteByGroupBlock,
        int judgeCount)
    {
        var groupBlocks = new List<GroupBlock>(athleteByGroupBlock.Count);

        foreach (var athleteBatch in athleteByGroupBlock)
        {
            var startTime = groupBlocks.Count > 0
                ? groupBlocks.Last().EndTime.Add(GroupBlockInterval)
                : contest.StartDate;

            var athletesByJudgeCount = SplitIntoBatches(athleteBatch, judgeCount);

            var groupBlock = new GroupBlock
            {
                ContestId = contest.Id,
                StartTime = startTime.ToUniversalTime()
            };

            var cells = CreateGroupBlockCells(athletesByJudgeCount, groupBlock).ToList();
            groupBlock.Schedule.AddRange(cells);
            groupBlock.EndTime = cells.Last().ExitTime;

            groupBlocks.Add(groupBlock);
        }

        return groupBlocks;
    }

    private static IEnumerable<ScheduledCell> CreateGroupBlockCells(List<List<Guid>> athletesByJudgeCount,
        GroupBlock groupBlock)
    {
        var sequenceNumber = 1;
        for (var index = 0; index < athletesByJudgeCount.Count; index++)
        {
            var exitTime = groupBlock.StartTime.Add(AthleteExitInterval * (index + 1));
            var athletesSubGroup = athletesByJudgeCount[index];

            foreach (var athleteId in athletesSubGroup)
            {
                var cell = new ScheduledCell
                {
                    SequenceNumber = sequenceNumber,
                    GroupBlock = groupBlock,
                    ExitTime = exitTime,
                    AthleteId = athleteId,
                };
                sequenceNumber++;

                yield return cell;
            }
        }
    }

    /// <summary>
    /// Валидация состояния соревнования.
    /// </summary>
    /// <param name="contest"></param>
    /// <exception cref="StepsBusinessException"></exception>
    private static void ValidateContestStatus(Contest? contest)
    {
        if (contest is null)
            throw new StepsBusinessException("Мероприятие не найдено");

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

    public async Task ReorderGroupBlock(ReorderGroupBlockViewModel reorderGroupBlockViewModel)
    {
        var groupBlockId = reorderGroupBlockViewModel.GroupBlockId;
        var orderedAthletes = reorderGroupBlockViewModel.Schedule.OrderBy(c => c.SequenceNumber).ToList();

        var repository = _unitOfWork.GetRepository<ScheduledCell>();
        
        var orderedSchedule = await repository.GetAllAsync(
                                  predicate: s => s.GroupBlockId.Equals(groupBlockId),
                                  orderBy: o => o.OrderBy(c => c.SequenceNumber),
                                  trackingType: TrackingType.Tracking)
                              ?? throw new StepsBusinessException("Групповой блок не найден");

        for (var i = 0; i < orderedSchedule.Count; i++)
        {
            orderedSchedule[i].AthleteId = orderedAthletes[i].AthleteId;
        }

        repository.Update(orderedSchedule);
        await _unitOfWork.SaveChangesAsync();
    }
}



// {
// "sequenceNumber": 1,
// "athleteId": "01956755-a3df-7d9c-a0e2-3339e3651c9e",
// },
// {
//     "sequenceNumber": 2,
//     "athleteId": "01956755-bad4-7a12-9492-dfabade663be",
// },
// {
//     "sequenceNumber": 3,
//     "athleteId": "01956755-c976-7bec-8f22-4ea250ac9916",
// },
// {
//     "sequenceNumber": 6,
//     "athleteId": "01956755-eeb6-7638-b208-293e010ca1a8",
// },
// {
//     "sequenceNumber": 5,
//     "athleteId": "01956755-d3e5-76bf-aa99-03025bfa5f3f",
// },
// {
//     "sequenceNumber": 4,
//     "athleteId": "01956755-dca4-791c-b846-df61f727a986",
// }