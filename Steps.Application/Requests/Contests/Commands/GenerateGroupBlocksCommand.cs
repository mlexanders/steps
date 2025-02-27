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
                
                var contest = await contestRepository.GetFirstOrDefaultAsync(c => c.Id == request.Id,
                    null,
                    c => c.Include(pre => pre.PreAthletesList),
                    TrackingType.NoTracking,
                    false,
                    false
                );
                
                var preAthletesList = contest.PreAthletesList;

                var athletes = preAthletesList.Athletes;

                var sortedAthletes = athletes.OrderBy(a => a.BirthDate).ToList();

                int athletesPerBlock = request.athletesCount;

                DateTime exitTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 9, 0, 0);

                int exitNumber = 1;

                var blocks = new List<GroupBlock>();
                int totalAthletes = sortedAthletes.Count;
                int totalBlocks = (int)Math.Ceiling((double)totalAthletes / athletesPerBlock);

                for (int blockIndex = 0; blockIndex < totalBlocks; blockIndex++)
                {
                    int athletesInCurrentBlock = (blockIndex == totalBlocks - 1)
                        ? totalAthletes - (blockIndex * athletesPerBlock)
                        : athletesPerBlock;

                    var blockAthletes = sortedAthletes
                        .Skip(blockIndex * athletesPerBlock)
                        .Take(athletesInCurrentBlock)
                        .ToList();

                    var groupBlock = new GroupBlock
                    {
                        ContestId = preAthletesList.ContestId,
                        Athletes = blockAthletes,
                        Numbers = new List<int>(),
                        ExitTime = exitTime
                    };

                    foreach (var athlete in blockAthletes)
                    {
                        athlete.ExitTime = exitTime;
                        groupBlock.Numbers.Add(exitNumber);
                        exitTime = exitTime.AddMinutes(2);
                        exitNumber++;
                    }

                    blocks.Add(groupBlock);
                }

                foreach (var block in blocks)
                {
                    await groupBlockRepository.InsertAsync(block);
                }

                contest.GroupBlocks.AddRange(blocks);

                contestRepository.Update(contest);

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
