using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.GroupBlocks.Commands;

public record DeleteByContestId(Guid ContestId) : IRequest<Result>;

public class DeleteByContestIdHandler : IRequestHandler<DeleteByContestId, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteByContestIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteByContestId request, CancellationToken cancellationToken)
    {
        var blocks = await _unitOfWork.GetRepository<GroupBlock>().GetAllAsync(
                        predicate: s => s.ContestId.Equals(request.ContestId),
                        trackingType: TrackingType.NoTracking)
                    ?? throw new StepsBusinessException("Блоки не найдены");

        _unitOfWork.GetRepository<GroupBlock>().Delete(blocks);
        await _unitOfWork.SaveChangesAsync();
        
        return Result.Ok();
    }
}
