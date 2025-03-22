using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Ratings;

namespace Steps.Application.Requests.Ratings;

public record GetRatingByBlockQuery(Guid GroupBlockId) : IRequest<Result<RatingViewModel>>;

public class
    GetPagedFinalScheduledCellsByGroupBlockIdQueryHandler : IRequestHandler<GetRatingByBlockQuery,
    Result<RatingViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedFinalScheduledCellsByGroupBlockIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RatingViewModel>> Handle(GetRatingByBlockQuery request,
        CancellationToken cancellationToken)
    {
        var rating = await _unitOfWork.GetRepository<Rating>().GetAllAsync(
                         predicate: r => r.GroupBlockId.Equals(request.GroupBlockId)
                                         && !r.IsComplete,
                         include: x =>
                             x.Include(s => s.Athlete),
                         trackingType: TrackingType.NoTracking)
                     ?? [];

        var ratingViewModel = new RatingViewModel()
        {
            NotCompleted = rating.ToList()
        };

        return Result<RatingViewModel>.Ok(ratingViewModel);
    }
}