using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Services.Api;

public class TeamsService : CrudService<Team, TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>, ITeamsService
{
    public TeamsService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.TeamsRoute())
    {
    }
}