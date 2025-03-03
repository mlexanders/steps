using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Services.Api;

public class ContestsesService : CrudService<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>, IContestsService
{
    public ContestsesService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.ContestsRoute())
    {
    }
}