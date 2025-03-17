using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ClubsFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.EntityFeature.ClubsFeature.Components;

public partial class ClubsManage : ManageBaseComponent<Club, ClubViewModel, CreateClubViewModel, UpdateClubViewModel>
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

    protected override async Task<Specification<Club>?> GetSpecification()
    {
        return null;
    }
}