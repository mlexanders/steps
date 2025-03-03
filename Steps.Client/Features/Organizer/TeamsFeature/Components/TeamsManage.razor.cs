using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Organizer.TeamsFeature.Services;
using Steps.Client.Features.Organizer.UsersFeature.Services;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Client.Features.Organizer.TeamsFeature.Components;

public partial class TeamsManage : ManageBaseComponent<TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>
{
    [Inject] protected TeamsManager TeamsManager { get; set; } = null!;
    [Inject] protected TeamsDialogManager TeamsDialogManager { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }

    protected override void OnInitialized()
    {
        Manager = TeamsManager;
        DialogManager = TeamsDialogManager;
        base.OnInitialized();
    }
}