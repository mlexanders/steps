using MediatR;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Application.Requests.PreSchedules.Command;

public record MarkAthleteCommand(MarkAthleteViewModel Model) : IRequest<Result>;

public class MarkAthleteCommandHandler : IRequestHandler<MarkAthleteCommand, Result>
{
    private readonly GroupBlockService _groupBlockService;

    public MarkAthleteCommandHandler(GroupBlockService groupBlockService)
    {
        _groupBlockService = groupBlockService;
    }

    public async Task<Result> Handle(MarkAthleteCommand request, CancellationToken cancellationToken)
    {
        await _groupBlockService.MarkAthlete(request.Model);
        return Result.Ok().SetMessage(request.Model.Confirmation ? "Участник отмечен" : "Явка участника была отозвана");
    }
}