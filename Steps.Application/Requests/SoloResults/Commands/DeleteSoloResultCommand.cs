using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;

namespace Steps.Application.Requests.SoloResults.Commands;

public record DeleteSoloResultCommand(Guid Id) : IRequest<Result>;

public class DeleteSoloResultCommandHandler : IRequestHandler<DeleteSoloResultCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSoloResultCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteSoloResultCommand request, CancellationToken cancellationToken)
    {
        var soloResult = await _unitOfWork.GetRepository<SoloResult>()
            .GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Id,
                trackingType: TrackingType.Tracking,
                cancellationToken: cancellationToken);

        if (soloResult == null)
            return Result.Fail("Результат не найден");

        _unitOfWork.GetRepository<SoloResult>().Delete(soloResult);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok().SetMessage("Результат удален");
    }
}
