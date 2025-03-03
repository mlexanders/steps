using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Client.Services.Api;

public class UsersService : CrudService<UserViewModel, CreateUserViewModel, UpdateUserViewModel>, IUsersService
{
    public UsersService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.UsersRoute())
    {
    }
}