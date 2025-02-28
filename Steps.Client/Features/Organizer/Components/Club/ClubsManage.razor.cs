using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Organizer.Services.Club;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.Organizer.Components.Club;

public partial class ClubsManage : ManageBaseComponent<ClubViewModel, CreateClubViewModel, UpdateClubViewModel>
{
    [Inject] protected ClubsManager ClubsManager { get; set; } = null!;
    [Inject] protected ClubsDialogManager ClubsDialogManager { get; set; } = null!;

    private List<ClubViewModel> _selectedClub;
    protected override void OnInitialized()
    {
        Manager = ClubsManager;
        DialogManager = ClubsDialogManager;
        base.OnInitialized();
    }
}