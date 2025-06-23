using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Client.Services.Authentication;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Client.Pages;

public partial class Login
{
    private readonly LoginViewModel _model = new();
    private string _error = string.Empty;
    private bool _errorVisible;
    private string _info = string.Empty;
    private bool _infoVisible;
    [Inject] private SecurityService SecurityService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private async Task Submit(LoginArgs args)
    {
        var model = new LoginViewModel
        {
            Login = args.Username,
            Password = args.Password
        };

        _errorVisible = false;
        _infoVisible = false;

        try
        {
            var result = await SecurityService.Login(model);
            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                _errorVisible = true;
                _error = result.Message ?? result.Errors.FirstOrDefault()?.Message ?? "Ошибка авторизации";
            }
        }
        catch (Exception ex)
        {
            _errorVisible = true;
            _error = $"Произошла ошибка";
        }
        finally
        {
            StateHasChanged();
        }
    }

    private async Task OnRegister()
    {
        NavigationManager.NavigateTo("/reg");
    }

    private async Task OnResetPassword()
    {
        // TODO: Реализовать сброс пароля
    }
}