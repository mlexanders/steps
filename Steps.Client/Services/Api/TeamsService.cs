using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Services.Api;

public class TeamsService : CrudService<TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>, ITeamsService
{
    public TeamsService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.TeamsRoute())
    {
    }
}