using MediatR;
using Steps.Shared;

namespace Steps.Application.Requests.GroupBlocks.Commands;

public class DeleteGroupBlock : IRequest<Result>
{
    public DeleteGroupBlock(Guid id)
    {
        throw new NotImplementedException();
    }
}