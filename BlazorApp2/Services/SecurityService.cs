using BlazorApp2.Services.Api;
using Steps.Domain.Base;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace BlazorApp2.Services;

public class SecurityService
{
    private readonly AccountService _accountService;
    private IUser? _currentUser;
    
    public event Action? OnUserChanged;

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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return null;
    }

    public async Task<Result<UserViewModel>> Login(LoginViewModel model)
    {
        var result = await _accountService.Login(model);
        if (result.Value is null) return result;
        
        _currentUser = result.Value;
        NotifyUserChanged();

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
        NotifyUserChanged();
    }

    private void NotifyUserChanged()
    {
        OnUserChanged?.Invoke();
    }
}