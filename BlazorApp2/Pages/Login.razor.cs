using BlazorApp2.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace BlazorApp2.Pages;

public partial class Login : ComponentBase
{
    private readonly LoginViewModel _model = new();
    private string _error;
    private bool _errorVisible;
    private string _info;
    private bool _infoVisible;

    [Inject] private SecurityService SecurityService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    //TODO:
    
    private async Task Submit(LoginArgs args)
    {
        var model = new LoginViewModel
        {
            Login = args.Username,
            Password = args.Password
        };

        try
        {
            var result = await SecurityService.Login(model);
            if (result.IsSuccess)
                NavigationManager.NavigateTo("/");
            _errorVisible = !result.IsSuccess;
            _error = result.Message ?? result.Errors.FirstOrDefault()?.Message ?? "Ошибка авторизации";
        }
        catch (HttpRequestException e)
        {
            _errorVisible = true;
            _error = "Ошибка сети";
        }
        catch (Exception e)
        {
            _errorVisible = true;
            _error = e.Message;
        }
        StateHasChanged();
    }


    private async Task OnResetPassword()
    {
    }
}