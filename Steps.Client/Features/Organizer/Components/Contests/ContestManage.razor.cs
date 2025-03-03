using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Organizer.Services.Contests;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.Organizer.Components.Contests;

public partial class
    ContestManage : ManageBaseComponent<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    [Inject] protected ContestManager ClubsManager { get; set; } = null!;
    [Inject] protected ContestDialogManager ContestDialogManager { get; set; } = null!;

    protected override void OnInitialized()
    {
        Manager = ClubsManager;
        DialogManager = ContestDialogManager;
        base.OnInitialized();
    }
}