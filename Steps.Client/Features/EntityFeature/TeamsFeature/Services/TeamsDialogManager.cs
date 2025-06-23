using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.TeamsFeature.Dialogs;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.TeamsFeature.Services;

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

        var dialogOptions = new DialogOptions
        {
            Width = "80vw",
            Height = "auto",
            ShowTitle = true,
            Resizable = false,
            Draggable = true,
            CloseDialogOnOverlayClick = false
        };

        var result = await _dialogService.OpenAsync<TeamCardDialog>(
            "Команда",
            options,
            dialogOptions);

        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog()
    {
        var result = await _dialogService.OpenAsync<CreateTeamDialog>("Создание команды");
        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog(ClubViewModel club)
    {
        var options = new Dictionary<string, object> { { "Model", club } };
        var result = await _dialogService.OpenAsync<CreateTeamDialog>("Создание команды", options);
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