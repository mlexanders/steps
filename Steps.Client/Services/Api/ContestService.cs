using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Services.Api;

public class ContestsService : CrudService<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>, IContestsService
{
    private readonly ApiRoutes.ContestsRoute _contestRoutes;

    public ContestsService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.ContestsRoute())
    {
        _contestRoutes = new ApiRoutes.ContestsRoute();
    }


    public Task<Result<List<ContestViewModel>>> GetByTimeInterval(GetContestByInterval criteria)
    {
        var path = _contestRoutes.GetByTimeInterval(criteria);
        return HttpClient.GetAsync<Result<List<ContestViewModel>>>(path);
    }

    public Task<Result> CloseCollectingEntries(Guid contestId)
    {
        throw new NotImplementedException();
    }
}