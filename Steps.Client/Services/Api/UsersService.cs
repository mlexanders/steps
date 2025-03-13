using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users;
using Steps.Shared.Contracts.Users.ViewModels;
using static Steps.Client.Services.Api.Routes.ApiRoutes;

namespace Steps.Client.Services.Api;

public class UsersService : CrudService<User, UserViewModel, CreateUserViewModel, UpdateUserViewModel>, IUsersService
{
    private readonly UsersRoute _usersRoute;

    public UsersService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.UsersRoute())
    {
        _usersRoute = new ApiRoutes.UsersRoute();
    }

    public Task<Result<PaggedListViewModel<UserViewModel>>> GetCounters(Page page)
    {
        var route = _usersRoute.GetCounters(page);
        return HttpClient.GetAsync<Result<PaggedListViewModel<UserViewModel>>>(route);
    }

    public Task<Result<PaggedListViewModel<UserViewModel>>> GetJudges(Page page)
    {
        var route = _usersRoute.GetJudges(page);
        return HttpClient.GetAsync<Result<PaggedListViewModel<UserViewModel>>>(route);
    }
}