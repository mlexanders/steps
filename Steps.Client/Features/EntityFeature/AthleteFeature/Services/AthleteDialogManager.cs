using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.AthleteFeature.Components;
using Steps.Client.Features.EntityFeature.AthleteFeature.Dialogs;
using Steps.Client.Features.EntityFeature.TeamsFeature.Dialogs;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.AthleteFeature.Services;

public class AthleteDialogManager : IDialogManager<AthleteViewModel>
{
    private readonly DialogService _dialogService;

    public AthleteDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> ShowCardDialog(AthleteViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };

        var dialogOptions = new DialogOptions
        {
            Width = "auto",
            Height = "auto",
            ShowTitle = true,
            Resizable = false,
            Draggable = true,
            CloseDialogOnOverlayClick = false
        };

        var result = await _dialogService.OpenAsync<AthleteCard>(
            "Спортсмен",
            options,
            dialogOptions);

        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog()
    {
        var result = await _dialogService.OpenAsync<CreateAthleteDialog>("Создание спортсмена");
        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog(TeamViewModel team)
    {
        var result = await _dialogService.OpenAsync<CreateAthleteDialog>(
            "Создание спортсмена",
            new Dictionary<string, object> { { "Team", team } }
        );
        return result ?? false;
    }

    public Task<bool> ShowUpdateDialog(AthleteViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowDeleteDialog(AthleteViewModel model)
    {
        throw new NotImplementedException();
    }
}