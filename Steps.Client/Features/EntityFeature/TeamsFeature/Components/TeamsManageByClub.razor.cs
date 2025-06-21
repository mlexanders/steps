using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.TeamsFeature.Services;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.TeamsFeature.Components;

public partial class
    TeamsManageByClub : ManageBaseComponent<Team, TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>
{
    [Inject] protected TeamsManager TeamsManager { get; set; } = null!;
    [Inject] protected TeamsDialogManager TeamsDialogManager { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }
    [Parameter] [Required] public ClubViewModel? Club { get; set; } = null!;
    
    [CascadingParameter] public IUser? User { get; set; }
    
    private Role? UserRole => User?.Role;

    protected override Task OnInitializedAsync()
    {
        Manager = TeamsManager;
        DialogManager = TeamsDialogManager;

        return base.OnInitializedAsync();
    }

    protected override async Task<Specification<Team>?> GetSpecification()
    {
        if (Club == null) return null;
        
        return new Specification<Team>().Where(t => t.ClubId == Club.Id);
    }

    protected override async Task OnCreate()
    {
        if (Club is null) return;
        
        var result = await TeamsDialogManager.ShowCreateDialog(Club);
        if (result) await Manager.LoadPage();
    }
}