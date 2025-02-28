using Steps.Client.Features.Organizer.Services;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.Users;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Client.Features.Organizer.UsersFeature.Services;

public class UsersManager : BaseEntityManager<UserViewModel, CreateUserViewModel, UpdateUserViewModel>
{
    public UsersManager(IUsersService contestsService) : base(contestsService)
    {
    }
}