using MediatR;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Application.Requests.GroupBlocks.Commands;

public class UpdateGroupBlock : IRequest<Result<Guid>>
{
    public UpdateGroupBlock(UpdateGroupBlockViewModel model)
    {
        throw new NotImplementedException();
    }
}