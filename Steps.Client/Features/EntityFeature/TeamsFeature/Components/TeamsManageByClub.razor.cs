using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Steps.Client.Features.EntityFeature.TeamsFeature.Services;
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
    [Parameter] [Required] public ClubViewModel Club { get; set; } = null!;

    protected override void OnInitialized()
    {
        try
        {
            Manager = TeamsManager;
            DialogManager = TeamsDialogManager;

            if (Club != null)
            {
                var specification = new Specification<Team>().Where(t => t.ClubId == Club.Id);
                TeamsManager.UseSpecification(specification);
            }


            base.OnInitialized();
        }
        catch (Exception ex) { }
    }

    protected override async Task OnCreate()
    {
        var result = await TeamsDialogManager.ShowCreateDialog(Club);
        if (result) await Manager.LoadPage();
    }
}