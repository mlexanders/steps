using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.Ratings;

public interface IRatingService
{
    Task<Result<RatingViewModel>> GetRatingsByBlock(Guid groupBlockId);
    Task<Result<DiplomasViewModel>> Complete(List<Rating> ratings);
}