using Radzen;
using Steps.Client.Features.Organizer.TeamsFeature.Dialogs;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Shared.Contracts.Users.ViewModels;
using DeleteUserDialog = Steps.Client.Features.Organizer.UsersFeature.Dialogs.DeleteUserDialog;
using UpdateUserDialog = Steps.Client.Features.Organizer.UsersFeature.Dialogs.UpdateUserDialog;
using UserCardDialog = Steps.Client.Features.Organizer.UsersFeature.Dialogs.UserCardDialog;

namespace Steps.Client.Features.Organizer.TeamsFeature.Services;

public class TeamsDialogManager : IDialogManager<TeamViewModel>
{
    private readonly DialogService _dialogService;

    public TeamsDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> ShowCardDialog(TeamViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<TeamCardDialog>("Команда", options);
        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog()
    {
        var result = await _dialogService.OpenAsync<CreateTeamDialog>("Создание команды");
        return result ?? false;
    }

    public async Task<bool> ShowUpdateDialog(TeamViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<UpdateTeamDialog>("Редактирование команды", options);
        return result ?? false;
    }

    public async Task<bool> ShowDeleteDialog(TeamViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService
            .OpenAsync<DeleteTeamDialog>("Вы уверены, что хотите удалить эту команду?", options);
        return result ?? false;
    }
}