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
                var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
                var contestRepository = _unitOfWork.GetRepository<Contest>();

                var contest = await contestRepository.GetFirstOrDefaultAsync(
                    c => c.Id == request.Id,
                    null,
                    c => c.Include(pre => pre.PreAthletesList)
                        .ThenInclude(pre => pre.Athletes)
                        .Include(c => c.Judjes),
                    TrackingType.Tracking,
                    false,
                    false
                );

                if (contest == null)
                    return Result.Fail("Соревнование не найдено.");

                var athletes = contest.PreAthletesList?.Athletes?
                    .OrderBy(a => a.BirthDate)
                    .ToList();

                if (athletes == null || !athletes.Any())
                    return Result.Fail("В предварительном списке нет спортсменов.");

                int judgesCount = contest.Judjes?.Count ?? 0;
                if (judgesCount == 0)
                    return Result.Fail("Нет назначенных судей.");

                int athletesPerBlock = request.athletesCount;
                DateTime currentExitTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month,
                    DateTime.UtcNow.Day, 9, 0, 0);
                int exitNumber = 1;

                var blocks = new List<GroupBlock>();
                int totalAthletes = athletes.Count;
                int totalBlocks = (int)Math.Ceiling((double)totalAthletes / athletesPerBlock);

                for (int blockIndex = 0; blockIndex < totalBlocks; blockIndex++)
                {
                    var blockAthletes = athletes
                        .Skip(blockIndex * athletesPerBlock)
                        .Take(athletesPerBlock)
                        .ToList();

                    var groupBlock = new GroupBlock
                    {
                        ContestId = contest.Id,
                        Athletes = new List<Athlete>(),
                        Numbers = new List<int>(),
                        ExitTime = DateTime.SpecifyKind(currentExitTime, DateTimeKind.Utc)
                    };

                    // Распределяем спортсменов внутри блока с интервалами
                    for (int i = 0; i < blockAthletes.Count; i++)
                    {
                        if (i > 0 && i % judgesCount == 0)
                        {
                            currentExitTime = currentExitTime.AddMinutes(2);
                        }

                        var athlete = blockAthletes[i];
                        athlete.ExitTime = DateTime.SpecifyKind(currentExitTime, DateTimeKind.Utc);
                        groupBlock.Athletes.Add(athlete);
                        groupBlock.Numbers.Add(exitNumber++);
                    }

                    blocks.Add(groupBlock);
                    currentExitTime = currentExitTime.AddMinutes(2); // Следующий блок начинается через 2 минуты
                }

                // Сохраняем блоки
                foreach (var block in blocks)
                {
                    await groupBlockRepository.InsertAsync(block);
                }

                contest.GroupBlocks = blocks;
                contestRepository.Update(contest);
                await _unitOfWork.SaveChangesAsync();

                return Result.Ok().SetMessage("Блоки сформированы!");
            }
            catch (Exception ex)
            {
                return Result.Fail($"Ошибка: {ex.Message}");
            }
        }
    }
}