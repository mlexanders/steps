using System;
using System.Threading.Tasks;
using Steps.Client.Services.Api;
using Steps.Domain.Base;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Client.Services;

public class SecurityService
{
    private readonly AccountService _accountService;
    private IUser? _currentUser;

    public event Action<IUser?>? OnUserChanged;

    public SecurityService(AccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<IUser?> GetCurrentUser()
    {
        if (_currentUser is not null) return _currentUser;

        try
        {
            var result = await _accountService.GetCurrentUser();
            return result.Value;
        }
        catch
        {
            /*ignore*/
        }

        return null;
    }

    public async Task<Result<UserViewModel>> Login(LoginViewModel model)
    {
        var result = await _accountService.Login(model);
        if (result.Value is null) return result;

        _currentUser = result.Value;
        NotifyUserChanged(_currentUser);

        return result;
    }

    public async Task Logout()
    {
        var result = await _accountService.Logout();
        if (!result.IsSuccess)
        {
            throw new InvalidOperationException($"Unable to log out: {result.Message}");
        }

        _currentUser = null;
        NotifyUserChanged(_currentUser);
    }

    private void NotifyUserChanged(IUser? resultValue)
    {
        OnUserChanged?.Invoke(resultValue);
    }
}