using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Client.Features.Organizer.Services;
using Steps.Client.Features.Organizer.Services.Contest;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.Organizer.Components.Contest;

public partial class ContestManage : ManageBaseComponent<ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    [Inject] protected ContestManager ClubsManager { get; set; } = null!;
    [Inject] protected ContestDialogManager ClubsDialogManager { get; set; } = null!;

    protected override void OnInitialized()
    {
        Manager = ClubsManager;
        DialogManager = ClubsDialogManager;
        base.OnInitialized();
    }
}