using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Shared;

namespace Steps.Application.Requests.GroupBlocks.Commands;

public record CreateFinalScheduleCommand(Guid GroupBlockId) : IRequest<Result>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}

public class CreateFinalScheduleCommandHandler : IRequestHandler<CreateFinalScheduleCommand, Result>
{
    private readonly GroupBlockService _groupBlockService;

    public CreateFinalScheduleCommandHandler(GroupBlockService groupBlockService)
    {
        _groupBlockService = groupBlockService;
    }

    public async Task<Result> Handle(CreateFinalScheduleCommand request, CancellationToken cancellationToken)
    {
        await _groupBlockService.GenerateFinalScheduleByGroupBlock(request.GroupBlockId, cancellationToken);

        return Result.Ok().SetMessage("Список создан");
    }
}
