using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ClubsFeature.Services;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.EntityFeature.ClubsFeature.Components;

public partial class ClubsManage : ManageBaseComponent<Club, ClubViewModel, CreateClubViewModel, UpdateClubViewModel>
{
    [Inject] protected ClubsManager ClubsManager { get; set; } = null!;
    [Inject] protected ClubsDialogManager ClubsDialogManager { get; set; } = null!;
    
    [CascadingParameter] public IUser? User { get; set; }
    
    private Role? UserRole => User?.Role;

    private List<ClubViewModel> _selectedClub;

    protected override void OnInitialized()
    {
        if (UserRole is Role.User)
        {
            var specification = new Specification<Club>().Where(c => c.Owner.Login == User.Login);
            
            ClubsManager.UseSpecification(specification);
        }
        
        Manager = ClubsManager;
        DialogManager = ClubsDialogManager;
        base.OnInitialized();
    }

    protected override async Task<Specification<Club>?> GetSpecification()
    {
        return null;
    }
}