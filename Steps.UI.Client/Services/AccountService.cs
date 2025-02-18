using Steps.Shared;
using Steps.Shared.Contracts.Accounts;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.UI.Client.Services;

public class AccountService : IAccountService
{
    private readonly HttpClientService _httpClientService;

    public AccountService(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<Result> Registration(RegistrationRequestViewModel model)
    {
        return await _httpClientService.PostAsync<Result, RegistrationRequestViewModel>(ApiRoutes.Auth.Register, model);
    }

    public async Task<Result<UserViewModel>> Login(LoginRequestViewModel model)
    {
        return await _httpClientService.PostAsync<Result<UserViewModel>, LoginRequestViewModel>(ApiRoutes.Auth.Login, model);
    }

    public async Task<Result<UserViewModel>> GetCurrentUser()
    {
        return await _httpClientService.GetAsync<Result<UserViewModel>, UserViewModel>(ApiRoutes.Auth.GetCurrentUser);
    }

    public async Task<Result> Logout()
    {
        return await _httpClientService.PostAsync<Result, object>(ApiRoutes.Auth.Logout, new { });
    }

    public async Task<Result> ConfirmAction(string token)
    {
        return await _httpClientService.PostAsync<Result, object>(ApiRoutes.Auth.ConfirmAction, new { });
    }
}