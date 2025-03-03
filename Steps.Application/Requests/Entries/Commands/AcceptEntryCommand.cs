using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Entries.Commands;

public record AcceptEntryCommand(Guid ModelId) : IRequest<Result>;

public class AcceptEntryCommandHandler : IRequestHandler<AcceptEntryCommand, Result>, IRequireAuthorization
{
    private readonly IUnitOfWork _unitOfWork;

    public AcceptEntryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AcceptEntryCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Entry>();

        var entry = await repository.GetFirstOrDefaultAsync(
            predicate: e => e.Id.Equals(request.ModelId),
            trackingType: TrackingType.Tracking) ?? throw new StepsBusinessException("Заявка не найдена");

        entry.IsSuccess = true;
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok().SetMessage("Заявка одобрена");
    }

    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}