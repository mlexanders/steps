using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.UsersFeature.Dialogs;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Client.Features.EntityFeature.UsersFeature.Services;

public class UsersDialogManager : IDialogManager<UserViewModel>
{
    private readonly DialogService _dialogService;

    public UsersDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async Task<bool> ShowCardDialog(UserViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<UserCardDialog>("Пользователь", options);
        return result ?? false;
    }

    public async Task<bool> ShowCreateDialog()
    {
        var result = await _dialogService.OpenAsync<CreateUserDialog>("Создание Пользователя");
        return result ?? false;
    }

    public async Task<bool> ShowUpdateDialog(UserViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService.OpenAsync<UpdateUserDialog>("Редактирование Пользователя", options);
        return result ?? false;
    }

    public async Task<bool> ShowDeleteDialog(UserViewModel model)
    {
        var options = new Dictionary<string, object> { { "Model", model } };
        var result = await _dialogService
            .OpenAsync<DeleteUserDialog>("Вы уверены, что хотите удалить этого Пользователя?", options);
        return result ?? false;
    }
}