using Steps.Client.Services.Api.Base;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Client.Services.Api;

public class AccountService : IAccountService
{
    private readonly HttpClientService _httpClient;

    public AccountService(HttpClientService httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result> Registration(RegistrationViewModel model)
    {
        return await _httpClient.PostAsync<Result, RegistrationViewModel>(ApiRoutes.Auth.Register, model);
    }

    public async Task<Result<UserViewModel>> Login(LoginViewModel model)
    {
        return await _httpClient.PostAsync<Result<UserViewModel>, LoginViewModel>(ApiRoutes.Auth.Login, model);
    }

    public async Task<Result<UserViewModel>> GetCurrentUser()
    {
        return await _httpClient.GetAsync<Result<UserViewModel>>(ApiRoutes.Auth.GetCurrentUser);
    }

    public async Task<Result> Logout()
    {
        return await _httpClient.PostAsync<Result, object>(ApiRoutes.Auth.Logout, new { });
    }

    public async Task<Result> ConfirmAction(string token)
    {
        return await _httpClient.PostAsync<Result, object>(ApiRoutes.Auth.ConfirmAction, new { });
    }
}