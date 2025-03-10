using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Services.Api;

public class GroupBlocksesService : IGroupBlocksService
{
    private readonly HttpClientService _httpClient;
    private readonly ApiRoutes.GroupBlockRoute _routes;

    public GroupBlocksesService(HttpClientService httpClient)
    {
        _httpClient = httpClient;
        _routes = new ApiRoutes.GroupBlockRoute();
    }

    public Task<Result<List<TeamViewModel>>> GetTeamsForCreateGroupBlocks(Guid contestId)
    {
        return _httpClient.GetAsync<Result<List<TeamViewModel>>>(_routes.GetTeamsForCreateGroupBlocks(contestId));
    }

    public Task CreateByTeams(CreateGroupBlockViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<GroupBlockViewModel>>> GetByContestId(Guid contestId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<GroupBlockViewModel>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteByContestId(Guid id)
    {
        throw new NotImplementedException();
    }
}