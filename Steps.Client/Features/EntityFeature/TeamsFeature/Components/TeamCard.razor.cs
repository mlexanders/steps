using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Steps.Client.Features.EntityFeature.TeamsFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.EntityFeature.TeamsFeature.Components;

public partial class TeamCard
{
    [Inject] protected TeamsManager TeamsManager { get; set; } = null!;
    [Inject] protected TeamsDialogManager TeamsDialogManager { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }
    [Parameter] [Required] public ClubViewModel? Club { get; set; } = null!;
    

    protected override async Task OnInitializedAsync()
    {
        var specification = new Specification<Team>();
        if (Club != null)
        {
            specification.Where(t => t.ClubId == Club.Id);
        }

        TeamsManager.UseSpecification(specification);

        await base.OnInitializedAsync();
    }

    protected async Task OnCreate()
    {
        var result = await TeamsDialogManager.ShowCreateDialog(Club);
        if (result) await TeamsManager.LoadPage();
    }
}