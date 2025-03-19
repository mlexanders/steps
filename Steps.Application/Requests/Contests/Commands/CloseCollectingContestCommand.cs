using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Requests.Contests.Queries;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Exceptions;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Contests.Commands;

public record CloseCollectingContestCommand(Guid ContestId) : IRequest<Result>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}

public class CloseCollectingContestCommandHandler : IRequestHandler<CloseCollectingContestCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public CloseCollectingContestCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(CloseCollectingContestCommand request, CancellationToken cancellationToken)

    {
        var contest = await _unitOfWork.GetRepository<Contest>()
                          .GetFirstOrDefaultAsync(
                              predicate: c => c.Id.Equals(request.ContestId),
                              trackingType: TrackingType.Tracking)
                      ?? throw new StepsBusinessException("Соревнование не найдено");
        
        contest.Status = ContestStatus.Closed;
        await _unitOfWork.SaveChangesAsync();
        
        return Result.Ok().SetMessage(contest.Status.GetDisplayName());
    }
}
