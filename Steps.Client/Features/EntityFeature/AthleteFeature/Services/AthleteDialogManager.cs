using Radzen;
using Steps.Client.Features.EntityFeature.AthleteFeature.Dialogs;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Client.Features.EntityFeature.AthleteFeature.Services;

public class AthleteDialogManager : IDialogManager<AthleteViewModel>
{
    private readonly DialogService _dialogService;

    public AthleteDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }
    
    public Task<bool> ShowCardDialog(AthleteViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ShowCreateDialog()
    {
        var result = await _dialogService.OpenAsync<CreateAthleteDialog>("Создание спортсмена");
        return result ?? false;
    }
    
    public async Task<bool> ShowCreateDialog(Guid teamId)
    {
        var result = await _dialogService.OpenAsync<CreateAthleteDialog>(
            "Создание спортсмена",
            new Dictionary<string, object> { { "TeamId", teamId } }
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