using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Dialogs;
using Steps.Client.Features.EntityFeature.TeamsFeature.Dialogs;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;

namespace Steps.Client.Features.EntityFeature.GroupBlocksFeature.Services;


public class GroupBlocksDialogManager : IDialogManager<GroupBlockViewModel>
{
    private readonly DialogService _dialogService;

    public GroupBlocksDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> ShowCardDialog(GroupBlockViewModel model)
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
    

    public async Task<bool> ShowUpdateDialog(GroupBlockViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<UpdateTeamDialog>("Редактирование команды", options);
        return result ?? false;
    }

    public async Task<bool> ShowDeleteDialog(GroupBlockViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService
            .OpenAsync<DeleteTeamDialog>("Вы уверены, что хотите удалить эту команду?", options);
        return result ?? false;
    }

    public async Task<GroupBlockViewModel> ShowSelectGroupBlockDialog(List<GroupBlockViewModel> groupBlocks)
    {
        var options = new Dictionary<string, object> { { "GroupBlocks", groupBlocks } };
        
        var result = await _dialogService.OpenAsync<SelectGroupBlockDialog>(
            "Выбор группового блока",
            options);
        
        return result;
    }
}