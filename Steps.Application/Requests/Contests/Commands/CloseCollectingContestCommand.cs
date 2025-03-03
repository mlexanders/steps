using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared;

namespace Steps.Application.Requests.Contests.Commands;

public record CloseCollectingContestCommand(Guid ModelId) : IRequest<Result>;

public class CloseCollectingContestCommandHandler : IRequestHandler<CloseCollectingContestCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public CloseCollectingContestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CloseCollectingContestCommand request, CancellationToken cancellationToken)
    {
        var modelId = request.ModelId;

        var contestRepository = _unitOfWork.GetRepository<Contest>();
        var preAthletesListRepository = _unitOfWork.GetRepository<PreAthletesList>();

        try
        {
            var contest = await contestRepository.GetFirstOrDefaultAsync(
                predicate: c => c.Id == modelId,
                include: q => q.Include(c => c.Entries)
                    .ThenInclude(a => a.Athletes),
                trackingType: TrackingType.Tracking
            );

            var preAthletesList = new PreAthletesList();

            preAthletesList.ContestId = contest.Id;
            preAthletesList.Contest = contest;

            var acceptedEntries = contest.Entries.Where(c => c.IsSuccess);

            foreach (var entry in acceptedEntries)
            {
                foreach (var athlete in entry.Athletes)
                {
                    preAthletesList.Athletes.Add(athlete);
                }
            }

            preAthletesListRepository.Insert(preAthletesList);

            contest.PreAthletesListId = preAthletesList.Id;
            contest.PreAthletesList = preAthletesList;

            contestRepository.Update(contest);

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok().SetMessage("Сбор заявок закрыт, предварительный список спортсменов готов!");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Ошибка при закрытии сбора заявок: {ex.Message}");
        }
    }
}