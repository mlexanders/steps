using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Contests.Commands;

public record DeleteContestCommand(Guid ModelId) : IRequest<Result>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}

public class DeleteEventCommandHandler : IRequestHandler<DeleteContestCommand, Result>
{
    private IUnitOfWork _unitOfWork;

    public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteContestCommand request, CancellationToken cancellationToken)
    {
        var modelId = request.ModelId;

        var repository = _unitOfWork.GetRepository<Contest>();

        var contest = await repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id.Equals(modelId),
            trackingType: TrackingType.Tracking
        );

        if (contest is null) throw new AppNotFoundException($"Мероприятие с ID {modelId} не найдено.");

        repository.Delete(contest);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok().SetMessage("Мероприятие удалено!");
    }
}