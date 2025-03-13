using Calabonga.UnitOfWork;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
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
    public async Task MarkAthlete(MarkAthleteViewModel model)
    {
        var cell = await _unitOfWork.GetRepository<ScheduledCell>()
                       .GetFirstOrDefaultAsync(
                           predicate: g => g.GroupBlockId.Equals(model.GroupBlockId)
                                           && g.AthleteId.Equals(model.AthleteId),
                           trackingType: TrackingType.Tracking)
                   ?? throw new StepsBusinessException("Участник в заданном блоке не найден");

        cell.IsConfirmed = model.Confirmation;
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// Генерирует ПРЕДВАРИТЕЛЬНЫЕ групповые блоки для соревнования.
    /// </summary>
    public async Task GenerateGroupBlocks(Contest contest, CreateGroupBlockViewModel model)
    {
        ValidateContestStatus(contest);
        
        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        var entryRepository = _unitOfWork.GetRepository<Entry>();

        var groupBlocksExist = await groupBlockRepository.ExistsAsync(gb => gb.ContestId.Equals(contest.Id));
        if (groupBlocksExist)
            throw new StepsBusinessException("Блоки уже сформированы.");

        var athleteEntries = await entryRepository.GetAllAsync(
            predicate: entry => entry.ContestId.Equals(contest.Id) && entry.IsSuccess,
            selector: entry => entry.Athletes.Select(a => new Athlete()
            {
                Id = a.Id,
                TeamId = a.TeamId,
                // добавить свойства по необходимости
            }),
            trackingType: TrackingType.NoTracking);

        var a = athleteEntries.ToList();
        //1. Все участники
        var athletes = a.SelectMany(x => x).DistinctBy(s => s.Id).ToList();
        
        // 2. Определяем порядок команд
        var teamsOrder = model.TeamsIds.Distinct().ToList();

        // 3. Сортируем по возрасту и порядку команд
        var sortedAthletes = athletes
            .OrderBy(a => a.BirthDate)
            .ThenBy(a => teamsOrder.IndexOf(a.TeamId))
            .ToList();

        // 4. Группируем участников по командам
        var teamsGrouped = sortedAthletes.GroupBy(a => a.TeamId)
            .OrderBy(g => teamsOrder.IndexOf(g.Key))
            .Select(g => g.ToList())
            .ToList();

        // 5. Разбиваем на блоки (по целым командам)
        var blocks = SplitIntoTeamBlocks(teamsGrouped, model.AthletesPerGroup);
        
        var judgeCount = contest.Judges?.Count ?? DefaultJudgesCount;
        if (judgeCount == 0) judgeCount = DefaultJudgesCount;

        // создания блока
        var groupBlocks = CreateGroupBlocks(contest, blocks, judgeCount);

        await groupBlockRepository.InsertAsync(groupBlocks);
        await _unitOfWork.SaveChangesAsync();
    }
    
    /// <summary>
    /// Меняет порядок участников в блоке. Принимает полный список  с новым порядок из блока, меняет участников местами.
    /// </summary>
    /// <param name="reorderGroupBlockViewModel">Модель на основе который происходит изменение порядка</param>
    /// <exception cref="StepsBusinessException">Если блок не найден</exception>
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

        var joinedConfirmation = orderedAthletes.Join(orderedSchedule, l => l.AthleteId, r => r.AthleteId, (newOrder, oldOrder) => oldOrder.IsConfirmed ).ToList();
        for (var i = 0; i < orderedSchedule.Count; i++)
        {
            var oldConfirmed = joinedConfirmation[i];
            orderedSchedule[i].IsConfirmed = oldConfirmed;
            orderedSchedule[i].AthleteId = orderedAthletes[i].AthleteId;
        }

        repository.Update(orderedSchedule);
        await _unitOfWork.SaveChangesAsync();
    }

    private static List<GroupBlock> CreateGroupBlocks(Contest contest, List<List<Athlete>>? athleteByGroupBlock,
        int judgeCount)
    {
        if (athleteByGroupBlock is null) 
            throw new StepsBusinessException("Ошибка при создании блока, список участников пуст для создания блоков");
        
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

    private static IEnumerable<ScheduledCell> CreateGroupBlockCells(List<List<Athlete>> athletesByJudgeCount,
        GroupBlock groupBlock)
    {
        var sequenceNumber = 1;
        for (var index = 0; index < athletesByJudgeCount.Count; index++)
        {
            var exitTime = groupBlock.StartTime.Add(AthleteExitInterval * (index + 1));
            var athletesSubGroup = athletesByJudgeCount[index];

            foreach (var athlete in athletesSubGroup)
            {
                var cell = new ScheduledCell
                {
                    SequenceNumber = sequenceNumber,
                    GroupBlock = groupBlock,
                    ExitTime = exitTime,
                    AthleteId = athlete.Id,
                };
                sequenceNumber++;

                yield return cell;
            }
        }
    }
    
    private List<List<Athlete>> SplitIntoTeamBlocks(List<List<Athlete>> teamGroups, int maxPerBlock)
    {
        var result = new List<List<Athlete>>();
        var currentBlock = new List<Athlete>();

        foreach (var team in teamGroups)
        {
            if (team.Count > maxPerBlock)
            {
                // Если команда больше maxPerBlock, создаем отдельный блок
                result.Add(new List<Athlete>(team));
            }
            else
            {
                // Если команда помещается в текущий блок, добавляем
                if (currentBlock.Count + team.Count > maxPerBlock)
                {
                    result.Add(currentBlock);
                    currentBlock = new List<Athlete>();
                }
                currentBlock.AddRange(team);
            }
        }

        // Добавляем последний блок, если есть данные
        if (currentBlock.Count > 0)
            result.Add(currentBlock);

        return result;
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
    private static IEnumerable<T> GetSortedAthletes<T>(IEnumerable<T> athleteIds)
    {
        return athleteIds; // TODO: 
    }
}