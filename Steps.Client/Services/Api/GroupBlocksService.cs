using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Services.Api;

public class GroupBlocksService : IGroupBlocksService
{
    private readonly HttpClientService _httpClient;
    private readonly ApiRoutes.GroupBlockRoute _routes;

    public GroupBlocksService(HttpClientService httpClient)
    {
        _httpClient = httpClient;
        _routes = new ApiRoutes.GroupBlockRoute();
    }

    public Task<Result<List<TeamViewModel>>> GetTeamsForCreateGroupBlocks(Guid contestId)
    {
        return _httpClient.GetAsync<Result<List<TeamViewModel>>>(_routes.GetTeamsForCreateGroupBlocks(contestId));
    }

    public Task<Result> CreateByTeams(CreateGroupBlockViewModel model)
    {
        return _httpClient.PostAsync<Result, CreateGroupBlockViewModel>(_routes.CreateByTeams, model);
    }

    public Task<Result<List<GroupBlockViewModel>>> GetByContestId(Guid contestId)
    {
        return _httpClient.GetAsync<Result<List<GroupBlockViewModel>>>(_routes.GetByContestId(contestId));
    }

    public Task<Result<GroupBlockViewModel>> GetById(Guid id)
    {
        return _httpClient.GetAsync<Result<GroupBlockViewModel>>(_routes.GetById(id));
    }

    public Task<Result> DeleteByContestId(Guid id)
    {
        return _httpClient.DeleteAsync<Result>(_routes.DeleteByContestId(id));
    }
}