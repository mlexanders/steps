using Calabonga.PagedListCore;
using Steps.Client.Services.Api.Base;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Services.Api;

public class ContestService : IContestService
{
    private readonly HttpClientService _httpClient;

    public ContestService(HttpClientService httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<Result<ContestViewModel>> Create(CreateContestViewModel createContestViewModel)
    {
        return _httpClient.PostAsync<Result<ContestViewModel>, CreateContestViewModel>(ApiRoutes.Contests.Create,
            createContestViewModel);
    }

    public Task<Result<Guid>> Update(UpdateContestViewModel updateContestViewModel)
    {
        return _httpClient.PatchAsync<Result<Guid>, UpdateContestViewModel>(ApiRoutes.Contests.Update,
            updateContestViewModel);
    }

    public Task<Result<ContestViewModel>> GetById(Guid clubId)
    {
        var route = ApiRoutes.Contests.GetById(clubId);
        return _httpClient.GetAsync<Result<ContestViewModel>>(route);
    }

    public Task<Result<PaggedListViewModel<ContestViewModel>>> GetPaged(Page page)
    {
        var route = ApiRoutes.Contests.GetPaged(page);
        return _httpClient.GetAsync<Result<PaggedListViewModel<ContestViewModel>>>(route);
    }

    public Task<Result> Delete(Guid clubId)
    {
        var route = ApiRoutes.Contests.Delete(clubId);
        return _httpClient.DeleteAsync<Result>(route);
    }
}