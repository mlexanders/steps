using Radzen;
using Steps.Client.Features.Organizer.Dialogs.Clubs;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.Organizer.Services.Club;

public class ClubsDialogManager : IDialogManager<ClubViewModel, CreateClubViewModel>
{
    private DialogService _dialogService;

    public ClubsDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> ShowCardDialog(CreateClubViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<ClubCardDialog>("Создание клуба", options);
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