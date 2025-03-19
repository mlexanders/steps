using MediatR;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Application.Requests.PreSchedules.Command;

public record ReorderGroupBlockCommand(ReorderGroupBlockViewModel Model) : IRequest<Result>;

public class ReorderGroupBlockCommandHandler : IRequestHandler<ReorderGroupBlockCommand, Result>
{
    private readonly GroupBlockService _groupBlockService;

    public ReorderGroupBlockCommandHandler(GroupBlockService groupBlockService)
    {
        _groupBlockService = groupBlockService;
    }

    public async Task<Result> Handle(ReorderGroupBlockCommand request, CancellationToken cancellationToken)
    {
        await _groupBlockService.ReorderGroupBlock(request.Model);
        return Result.Ok().SetMessage("Список обновлен");
    }
}