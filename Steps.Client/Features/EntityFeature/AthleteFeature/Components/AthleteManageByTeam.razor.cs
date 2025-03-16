using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.AthleteFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.AthleteFeature.Components;

public partial class AthleteManageByTeam :  ManageBaseComponent<Athlete, AthleteViewModel, CreateAthleteViewModel,UpdateAthleteViewModel>
{
    [Inject] protected AthleteManager AthleteManager { get; set; } = null!;
    [Inject] protected AthleteDialogManager AthleteDialogManager { get; set; } = null!;

    public TeamViewModel? Team { get; set; }
    
    private IList<AthleteViewModel> _selected;

    protected override async Task OnInitializedAsync()
    {
        Manager = AthleteManager;
        DialogManager = AthleteDialogManager;
        await base.OnInitializedAsync();
    }

    protected override async Task<Specification<Athlete>?> GetSpecification()
    {
        if (Team is null) return null;
        
        return new Specification<Athlete>().Where(a => a.TeamId.Equals(Team.Id));
    }
}