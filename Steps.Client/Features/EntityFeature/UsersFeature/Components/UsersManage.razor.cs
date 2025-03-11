using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.UsersFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Client.Features.EntityFeature.UsersFeature.Components;

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