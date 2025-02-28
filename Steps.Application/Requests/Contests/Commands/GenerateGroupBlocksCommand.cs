using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared;
using Steps.Shared.Contracts.AthletesLists.PreAthletesList.ViewModels;

namespace Steps.Application.Requests.Contests.Commands
{
    public record GenerateGroupBlocksCommand(Guid Id, int athletesCount) : IRequest<Result>;

    public class GenerateGroupBlocksCommandHandler : IRequestHandler<GenerateGroupBlocksCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenerateGroupBlocksCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GenerateGroupBlocksCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var preAthletesListRepository = _unitOfWork.GetRepository<PreAthletesList>();
                var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
                var contestRepository = _unitOfWork.GetRepository<Contest>();

                // Получаем соревнование с предварительным списком спортсменов и судьями
                var contest = await contestRepository.GetFirstOrDefaultAsync(
                    c => c.Id == request.Id,
                    null,
                    c => c.Include(pre => pre.PreAthletesList)
                            .ThenInclude(pre => pre.Athletes)
                            .Include(c => c.Judjes), // Включаем судей
                    TrackingType.NoTracking,
                    false,
                    false
                );

                if (contest == null)
                {
                    return Result.Fail("Соревнование не найдено.");
                }

                var preAthletesList = contest.PreAthletesList;

                if (preAthletesList == null || preAthletesList.Athletes == null || !preAthletesList.Athletes.Any())
                {
                    return Result.Fail("В предварительном списке нет спортсменов.");
                }

                var athletes = preAthletesList.Athletes;

                // Сортируем спортсменов по дате рождения
                var sortedAthletes = athletes.OrderBy(a => a.BirthDate).ToList();

                // Количество судей
                int judgesCount = contest.Judjes?.Count ?? 0;
                if (judgesCount == 0)
                {
                    return Result.Fail("Нет назначенных судей для соревнования.");
                }

                // Желаемое количество спортсменов в блоке (из запроса)
                int athletesPerBlock = request.athletesCount;

                // Время начала сдачи зачета
                DateTime exitTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 9, 0, 0);

                int exitNumber = 1;

                var blocks = new List<GroupBlock>();
                int totalAthletes = sortedAthletes.Count();

                // Рассчитываем общее количество блоков
                int totalBlocks = (int)Math.Ceiling((double)totalAthletes / athletesPerBlock);

                for (int blockIndex = 0; blockIndex < totalBlocks; blockIndex++)
                {
                    // Количество спортсменов в текущем блоке
                    int athletesInCurrentBlock = (blockIndex == totalBlocks - 1)
                        ? totalAthletes - (blockIndex * athletesPerBlock)
                        : athletesPerBlock;

                    // Получаем спортсменов для текущего блока
                    var blockAthletes = sortedAthletes
                        .Skip(blockIndex * athletesPerBlock)
                        .Take(athletesInCurrentBlock)
                        .ToList();

                    // Разбиваем спортсменов на подгруппы по количеству судей
                    for (int judgeGroupIndex = 0; judgeGroupIndex < (int)Math.Ceiling((double)athletesInCurrentBlock / judgesCount); judgeGroupIndex++)
                    {
                        // Количество спортсменов в текущей подгруппе
                        int athletesInJudgeGroup = (judgeGroupIndex == (int)Math.Ceiling((double)athletesInCurrentBlock / judgesCount) - 1)
                            ? athletesInCurrentBlock - (judgeGroupIndex * judgesCount)
                            : judgesCount;

                        // Получаем спортсменов для текущей подгруппы
                        var judgeGroupAthletes = blockAthletes
                            .Skip(judgeGroupIndex * judgesCount)
                            .Take(athletesInJudgeGroup)
                            .ToList();

                        // Создаем блок для текущей подгруппы
                        var groupBlock = new GroupBlock
                        {
                            ContestId = preAthletesList.ContestId,
                            Athletes = judgeGroupAthletes,
                            Numbers = new List<int>(),
                            ExitTime = exitTime
                        };

                        // Назначаем время выхода и номера спортсменам
                        foreach (var athlete in judgeGroupAthletes)
                        {
                            athlete.ExitTime = exitTime;
                            groupBlock.Numbers.Add(exitNumber);
                            exitNumber++;
                        }

                        // Добавляем блок в список
                        blocks.Add(groupBlock);

                        // Увеличиваем время выхода на 2 минуты для следующей подгруппы
                        exitTime = exitTime.AddMinutes(2);
                    }
                }

                // Сохраняем блоки в базу данных
                foreach (var block in blocks)
                {
                    await groupBlockRepository.InsertAsync(block);
                }

                // Обновляем соревнование с новыми блоками
                contest.GroupBlocks ??= new List<GroupBlock>();
                contest.GroupBlocks.AddRange(blocks);

                contestRepository.Update(contest);

                // Сохраняем изменения в базе данных
                await _unitOfWork.SaveChangesAsync();

                return Result.Ok().SetMessage("Список спортсменов сформирован!");
            }
            catch (Exception ex)
            {
                return Result.Fail($"Ошибка при формировании списка спортсменов: {ex.Message}");
            }
        }
    }
}
