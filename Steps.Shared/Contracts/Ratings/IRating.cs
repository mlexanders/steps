using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.Ratings;

public interface IRatingService
{
    Task<Result<RatingViewModel>> GetRatingByBlock(Guid groupBlockId);
}

public class RatingViewModel
{
    public List<Rating> NotCompleted { get; set; } = [];
}