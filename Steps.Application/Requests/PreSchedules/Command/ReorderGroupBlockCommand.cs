using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Services;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Application.Requests.PreSchedules.Command;

public record ReorderGroupBlockCommand(ReorderGroupBlockViewModel Model) : IRequest<Result>
    , IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}

public class ReorderGroupBlockCommandHandler : IRequestHandler<ReorderGroupBlockCommand, Result>
{
    private readonly SchedulesService _schedulesService;

    public ReorderGroupBlockCommandHandler(SchedulesService schedulesService)
    {
        _schedulesService = schedulesService;
    }

    public async Task<Result> Handle(ReorderGroupBlockCommand request, CancellationToken cancellationToken)
    {
        await _schedulesService.ReorderGroupBlock(request.Model);
        return Result.Ok().SetMessage("Список обновлен");
    }
}