using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Services;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.GroupBlocksFeature.Components;

public partial class GroupBlocksManage : BaseNotificate
{
    [Inject] protected GroupBlocksDialogManager GroupBlocksDialogManager { get; set; } = null!;
    [Inject] protected IGroupBlocksService GroupBlocksService { get; set; } = null!;

    [Parameter] [Required] public ContestViewModel? Contest { get; set; }

    private List<TeamViewModel>? _teams;
    private List<GroupBlockViewModel>? _blocks;
    
    protected override async Task OnInitializedAsync()
    {
        if (Contest is null) return;
        
        var result = await GroupBlocksService.GetByContestId(Contest.Id);
        if (result?.IsSuccess != true) ShowResultMessage(result);

        _blocks = result?.Value ?? [];
    }

    private static string GetTittle(GroupBlockViewModel block)
    {
        var start = block.StartTime.ToString("t");
        var end = block.EndTime.ToString("t");
        return $"{start} - {end}";
    }

    private async Task GetTeams()
    {
        var result = await GroupBlocksService.GetTeamsForCreateGroupBlocks(Contest.Id);
        if (result?.IsSuccess != true) ShowResultMessage(result);
        _teams = result?.Value ?? [];
    }

    private async Task CreateBlocks()
    {
        var createGroupBlockViewModel = new CreateGroupBlockViewModel
        {
            ContestId = Contest.Id,
            AthletesPerGroup = 3,
            TeamsIds = _teams.Select(t => t.Id).ToList(),
        };
        
        var result = await GroupBlocksService.CreateByTeams(createGroupBlockViewModel);
        ShowResultMessage(result);
        StateHasChanged();
    }

    private async Task OnDeleteBlocks()
    {
        var result = await GroupBlocksService.DeleteByContestId(Contest.Id);
        ShowResultMessage(result);
        StateHasChanged();
    }
}