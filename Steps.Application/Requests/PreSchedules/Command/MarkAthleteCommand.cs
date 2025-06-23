using MediatR;
using Steps.Application.Services;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Application.Requests.PreSchedules.Command;

public record MarkAthleteCommand(MarkAthleteViewModel Model) : IRequest<Result>;

public class MarkAthleteCommandHandler : IRequestHandler<MarkAthleteCommand, Result>
{
    private readonly SchedulesService _schedulesService;

    public MarkAthleteCommandHandler(SchedulesService schedulesService)
    {
        _schedulesService = schedulesService;
    }

    public async Task<Result> Handle(MarkAthleteCommand request, CancellationToken cancellationToken)
    {
        await _schedulesService.MarkAthlete(request.Model);
        return Result.Ok().SetMessage(request.Model.Confirmation ? "Участник отмечен" : "Явка участника была отозвана");
    }
}