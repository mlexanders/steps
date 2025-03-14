using Steps.Client.Services.Api;
using Steps.Domain.Base;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Timer = System.Timers.Timer;

namespace Steps.Client.Services.Authentication;

public class SecurityService : IDisposable
{
    private readonly AccountService _accountService;
    private IUser? _currentUser;
    private readonly Timer _refreshTimer;

    public event Action<IUser?>? OnUserChanged;

    public SecurityService(AccountService accountService)
    {
        _accountService = accountService;
       _refreshTimer = new Timer(TimeSpan.FromMinutes(1)); // TODO:
       _refreshTimer.Elapsed += (e, v) => _ = Refresh();
    }

    private async Task Refresh()
    {
        var refreshedUser = await GetCurrentUserRequest();

        if (refreshedUser is not null && refreshedUser.Id == _currentUser?.Id) return; 
        
        _currentUser = refreshedUser;
        _refreshTimer.Stop();
        NotifyUserChanged(_currentUser);
    }

    public async Task<IUser?> GetCurrentUser()
    {
        if (_currentUser is not null) return _currentUser;

        return await GetCurrentUserRequest();
    }

    private async Task<IUser?> GetCurrentUserRequest()
    {
        try
        {
            var result = await _accountService.GetCurrentUser();
            return result.Value;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Result<UserViewModel>> Login(LoginViewModel model)
    {
        var result = await _accountService.Login(model);
        if (result.Value is null) return result;

        _currentUser = result.Value;
        NotifyUserChanged(_currentUser);
        _refreshTimer.Start();
        return result;
    }

    public async Task Logout()
    {
        var result = await _accountService.Logout();
        if (!result.IsSuccess)
        {
            throw new InvalidOperationException($"Unable to log out: {result.Message}");
        }

        _refreshTimer.Stop();
        _currentUser = null;
        NotifyUserChanged(_currentUser);
    }

    private void NotifyUserChanged(IUser? resultValue)
    {
        OnUserChanged?.Invoke(resultValue);
    }

    public void Dispose()
    {
        _refreshTimer.Dispose();
    }
}