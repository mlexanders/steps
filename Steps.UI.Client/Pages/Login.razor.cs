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

    [Inject] private SecurityService SecurityService { get; set; }
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
            var result = await SecurityService.Login(model);
            _errorVisible = !result.IsSuccess;
            _error = result.Message ?? result.Errors.FirstOrDefault()?.Message ?? "Ошибка авторизации";
            // NavigationManager.NavigateTo("/", false);
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


    private async Task OnReset()
    {
    }

    private Task InvalidSubmit(FormInvalidSubmitEventArgs formInvalidSubmitEventArgs)
    {
        var e = formInvalidSubmitEventArgs.Errors;
        return Task.CompletedTask;
    }
}