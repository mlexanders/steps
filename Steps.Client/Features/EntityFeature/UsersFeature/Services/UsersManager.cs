using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Client.Features.EntityFeature.UsersFeature.Services;

public class UsersManager : EntityManagerBase<User, UserViewModel, CreateUserViewModel, UpdateUserViewModel>
{
    public UsersManager(IUsersService contestsService) : base(contestsService)
    {
    }
}