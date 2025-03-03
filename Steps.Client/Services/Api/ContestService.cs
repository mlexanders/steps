using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Services.Api;

public class ContestsesService : CrudService<ContestViewModel, CreateContestViewModel, UpdateContestViewModel>, IContestsService
{
    public ContestsesService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.ContestsRoute())
    {
    }

    public Task<Result> GenerateGroupBlocks(Guid contestId, int athletesCount)
    {
        throw new NotImplementedException();
    }

    public Task<Result> CheckAthlete(Guid athleteId, Guid contestId, bool isAppeared)
    {
        throw new NotImplementedException();
    }

    public Task<Result> CloseCollectingEntries(Guid contestId)
    {
        throw new NotImplementedException();
    }
}