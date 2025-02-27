using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;

namespace Steps.Application.Requests.Contests.Commands;

public record CheckAthleteCommand(Guid AthleteId, Guid ContestId, bool? isAppeared) : IRequest<Result>;
    
public class CheckAthleteCommandHandler : IRequestHandler<CheckAthleteCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CheckAthleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CheckAthleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var athleteRepository = _unitOfWork.GetRepository<Athlete>();
            var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();

            var athlete = await athleteRepository.GetFirstOrDefaultAsync(
                a => a.Id == request.AthleteId,
                null,
                null,
                TrackingType.Tracking,
                false,
                false);

            if (athlete == null)
                return Result.Fail("Спортсмен не найден.");

            var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
                gb => gb.Athletes.Any(a => a.Id == athlete.Id),
                null,
                gb => gb.Include(a => a.Athletes),
                TrackingType.Tracking,
                false,
                false);

            if (groupBlock == null)
                return Result.Fail("Блок спортсмена не найден.");

            athlete.IsAppeared = request.isAppeared;
            var blockAthletes = groupBlock.Athletes.OrderBy(a => a.ExitTime).ToList();
            int athleteIndex = blockAthletes.FindIndex(a => a.Id == athlete.Id);

            if (athleteIndex > 0)
            {
                var previousAthlete = blockAthletes[athleteIndex - 1];

                if (previousAthlete.IsAppeared.HasValue && previousAthlete.IsAppeared == false && request.isAppeared == true)
                {
                    int shiftedIndex = blockAthletes.FindIndex(a => a.ExitTime == previousAthlete.ExitTime);

                    if (shiftedIndex > athleteIndex) 
                    {
                        var originalTime = previousAthlete.ExitTime ?? (athlete.ExitTime?.AddMinutes(-2) ?? DateTime.UtcNow);
                        for (int i = shiftedIndex; i > athleteIndex; i--)
                        {
                            blockAthletes[i].ExitTime = blockAthletes[i - 1].ExitTime;
                        }
                        previousAthlete.ExitTime = originalTime;
                    }
                }

                if (previousAthlete.IsAppeared.HasValue && previousAthlete.IsAppeared == false)
                {
                    athlete.ExitTime = previousAthlete.ExitTime;
                }
            }

            for (int i = athleteIndex + 1; i < blockAthletes.Count; i++)
            {
                blockAthletes[i].ExitTime = (blockAthletes[i - 1].ExitTime ?? DateTime.UtcNow).AddMinutes(2);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok().SetMessage("Спортсмен отмечен как прибывший, расписание обновлено.");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Ошибка при обновлении статуса спортсмена: {ex.Message}");
        }
    }
}