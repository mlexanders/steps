using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Organizer.Services.Contest;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.Organizer.Components.Contest;

public partial class
    ContestManage : ManageBaseComponent<ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
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