using Steps.Shared;
using Steps.Shared.Contracts.Ratings;

namespace Steps.Client.Services.Api;

public class RatingService : IRatingService
{
    public Task<Result<RatingViewModel>> GetRatingByBlock(Guid groupBlockId)
    {
        throw new NotImplementedException();
    }
}