using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Organizer.UsersFeature.Services;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users.ViewModels;
using Steps.Domain.Entities;

namespace Steps.Client.Features.Organizer.UsersFeature.Components;

public partial class UsersManage : ManageBaseComponent<User, UserViewModel, CreateUserViewModel, UpdateUserViewModel>
{
    [Inject] protected UsersManager UsersManager { get; set; } = null!;
    [Inject] protected UsersDialogManager UsersDialogManager { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }

    protected override void OnInitialized()
    {
        Manager = UsersManager;
        DialogManager = UsersDialogManager;
        base.OnInitialized();
    }
}