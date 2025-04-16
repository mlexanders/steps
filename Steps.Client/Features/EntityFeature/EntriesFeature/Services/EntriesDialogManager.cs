using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.AthleteFeature.Dialogs;
using Steps.Client.Features.EntityFeature.EntriesFeature.Components;
using Steps.Client.Features.EntityFeature.EntriesFeature.Dialogs;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Services;

public class EntriesDialogManager : IDialogManager<EntryViewModel>
{
    private readonly DialogService _dialogService;

    public EntriesDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }
    
    public async Task<bool> ShowCardDialog(EntryViewModel model)
    {
        var result = await _dialogService.OpenAsync<EntryCard>("Детали заявки",
            new Dictionary<string, object> { { "Model", model } },
            new DialogOptions { Width = "600px", CloseDialogOnOverlayClick = true });
        
        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog()
    {
        var result = await _dialogService.OpenAsync<CreateEntryDialog>("Создание заявки");
        return result ?? false;
    }
    
    public async Task<bool> ShowCreateDialog(Guid contestId)
    {
        var result = await _dialogService.OpenAsync<CreateEntryDialog>(
            "Создание заявки",
            new Dictionary<string, object> { { "ContestId", contestId } }
        );

        return result ?? false;
    }

    public Task<bool> ShowUpdateDialog(EntryViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowDeleteDialog(EntryViewModel model)
    {
        throw new NotImplementedException();
    }
}