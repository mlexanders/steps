using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Ratings;

namespace Steps.Client.Services.Api;

public class RatingService : IRatingService
{
    private readonly HttpClientService _httpClient;
    private readonly ApiRoutes.RatingsRoute _routes;

    public RatingService(HttpClientService httpClient)
    {
        _httpClient = httpClient;
        _routes = new ApiRoutes.RatingsRoute();
    }

    public Task<Result<RatingViewModel>> GetRatingByBlock(Guid groupBlockId)
    {
        var path = _routes.GetRatingByBlock(groupBlockId);
        return _httpClient.GetAsync<Result<RatingViewModel>>(path);
    }

    public Task<Result<DiplomasViewModel>> Complete(List<Rating> ratings)
    {
        var path = _routes.CreateDiplomas;
        return _httpClient.PostAsync<Result<DiplomasViewModel>, List<Rating>>(path, ratings);
    }
}