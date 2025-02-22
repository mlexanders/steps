using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.UI.Client.Services;
using Steps.UI.Client.Services.Api;

namespace Steps.UI.Client.Pages;

public partial class Login : ComponentBase
{
    private readonly LoginViewModel _model = new();
    private string _error;
    private bool _errorVisible;
    private string _info;
    private bool _infoVisible;

    [Inject] private ApplicationAuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private AccountService AccountService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] public DialogService DialogService { get; set; }


    private async Task Submit(LoginArgs args)
    {
        var model = new LoginViewModel
        {
            Login = args.Username,
            Password = args.Password
        };

        try
        {
            await AccountService.Login(model);
            NavigationManager.NavigateTo("/", false);
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
    }


    private async Task OnReset()
    {
    }
}