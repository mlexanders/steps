using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.Ratings;

public interface IRatingService
{
    Task<Result<RatingViewModel>> GetRatingByBlock(Guid groupBlockId);
    Task<Result<DiplomasViewModel>> Complete(List<Rating> ratings);
}

public class DiplomasViewModel
{
    public string Url { get; set; }
}