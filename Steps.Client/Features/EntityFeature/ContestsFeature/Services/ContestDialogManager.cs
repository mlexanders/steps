using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ContestsFeature.Dialogs;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Services;

public class ContestDialogManager : IDialogManager<ContestViewModel>
{
    private readonly DialogService _dialogService;

    public ContestDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> ShowCardDialog(ContestViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var dialogOptions = new DialogOptions
        {
            Width = "60vw",
            Height = "auto",
            ShowTitle = true,
            Resizable = false,
            Draggable = true,
            CloseDialogOnOverlayClick = false
        };

        var result = await _dialogService.OpenAsync<ContestCardDialog>("Мероприятие", options, dialogOptions);
        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog()
    {
        var dialogOptions = new DialogOptions
        {
            Width = "60vw",
            Height = "auto",
            ShowTitle = true,
            Resizable = false,
            Draggable = true,
            CloseDialogOnOverlayClick = false
        };

        var result = await _dialogService.OpenAsync<CreateContestDialog>("Создание мероприятия", options: dialogOptions);
        return result ?? false;
    }

    public async Task<bool> ShowUpdateDialog(ContestViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<UpdateContestDialog>("Редактирование мероприятия", options);
        return result ?? false;
    }

    public async Task<bool> ShowDeleteDialog(ContestViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService
            .OpenAsync<DeleteContestDialog>("Вы уверены, что хотите удалить это мероприятие?", options);
        return result ?? false;
    }
}