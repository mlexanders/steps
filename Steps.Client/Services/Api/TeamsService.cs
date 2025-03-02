using System.Linq.Expressions;
using System.Web;
using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Services.Api;

public class TeamsService : CrudService<TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>, ITeamsService
{
    public TeamsService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.TeamsRoute())
    {
    }

    // public Task<Result<TeamViewModel>> GetBy(Expression<Func<TeamViewModel, bool>> predicate)
    // {
    //     
    // }
}