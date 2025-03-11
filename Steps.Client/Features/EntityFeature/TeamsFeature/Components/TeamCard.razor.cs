using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.AthleteFeature.Dialogs;
using Steps.Client.Features.EntityFeature.AthleteFeature.Services;
using Steps.Client.Features.EntityFeature.TeamsFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.TeamsFeature.Components;

public partial class TeamCard: ManageBaseComponent<Team, TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>
{
    [Inject] protected TeamsManager TeamsManager { get; set; } = null!;
    [Inject] protected AthleteManager AthleteManagerEntityManager { get; set; } = null!;
    [Inject] protected TeamsDialogManager TeamsDialogManager { get; set; } = null!;
    [Inject] protected AthleteDialogManager AthleteDialogManager { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }
    [Parameter] [Required] public ClubViewModel Club { get; set; } = null!;
    
    private CreateAthleteDialog _createAthleteDialog;

    protected override async Task OnInitializedAsync()
    {
        Manager = TeamsManager;
        DialogManager = TeamsDialogManager;

        var specification = new Specification<Team>()
            .Include(a => a.Include(a => a.Athletes));

        if (Club != null)
        {
            specification = specification.Where(t => t.ClubId == Club.Id);
        }

        TeamsManager.UseSpecification(specification);

        await base.OnInitializedAsync();
    }

    protected override async Task OnCreate()
    {
        var result = await TeamsDialogManager.ShowCreateDialog(Club);
        if (result) await Manager.LoadPage();
    }
    
    protected async Task OpenCreateAthleteDialog()
    {
        var result = await AthleteDialogManager.ShowCreateDialog(Model.Id);
        if (result) await Manager.LoadPage();
    }
}