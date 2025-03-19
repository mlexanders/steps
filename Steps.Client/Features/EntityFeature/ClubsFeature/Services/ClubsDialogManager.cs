using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ClubsFeature.Dialogs;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.EntityFeature.ClubsFeature.Services;

public class ClubsDialogManager : IDialogManager<ClubViewModel>
{
    private readonly DialogService _dialogService;

    public ClubsDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> ShowCardDialog(ClubViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<ClubCardDialog>("Клуб", options);
        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog()
    {
        var result = await _dialogService.OpenAsync<CreateClubDialog>("Создание клуба");
        return result ?? false;
    }

    public async Task<bool> ShowUpdateDialog(ClubViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<UpdateClubDialog>("Редактирование клуба", options);
        return result ?? false;
    }

    public async Task<bool> ShowDeleteDialog(ClubViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService
            .OpenAsync<DeleteClubDialog>("Вы уверены, что хотите удалить этот клуб?", options);
        return result ?? false;
    }
}