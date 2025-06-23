using MediatR;
using Steps.Application.Services;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Application.Requests.PreSchedules.Command;

public record ReorderGroupBlockCommand(ReorderGroupBlockViewModel Model) : IRequest<Result>;

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