using Calabonga.UnitOfWork;
using MediatR.Pipeline;

namespace Steps.Application.Behaviors;

public class UnitOfWorkPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkPostProcessor(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        if (_unitOfWork.LastSaveChangesResult.IsOk) return Task.CompletedTask;
        throw _unitOfWork.LastSaveChangesResult.Exception!;
    }
}