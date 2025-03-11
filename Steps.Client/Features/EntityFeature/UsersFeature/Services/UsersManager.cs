using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Client.Features.EntityFeature.UsersFeature.Services;

public class UsersManager : EntityManagerBase<User, UserViewModel, CreateUserViewModel, UpdateUserViewModel>
{
    private readonly IUsersService _usersService;

    public UsersManager(IUsersService contestsService) : base(contestsService)
    {
        _usersService = contestsService;
    }

    public async Task<Result<PaggedListViewModel<UserViewModel>>> GetCounters(Page page)
    {
        try
        {
            return await _usersService.GetCounters(page);
        }
        catch (Exception e)
        {
            return Result<PaggedListViewModel<UserViewModel>>.Fail(e.Message);
        }
    }

    public async Task<Result<PaggedListViewModel<UserViewModel>>> GetJudges(Page page)
    {
        try
        {
            return await _usersService.GetJudges(page);
        }
        catch (Exception e)
        {
            return Result<PaggedListViewModel<UserViewModel>>.Fail(e.Message);
        }
    }
}