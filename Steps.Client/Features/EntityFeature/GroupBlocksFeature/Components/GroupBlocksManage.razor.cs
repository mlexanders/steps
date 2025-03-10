using Microsoft.AspNetCore.Components;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Services;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Client.Features.EntityFeature.GroupBlocksFeature.Components;

public partial class GroupBlocksManage : ManageBaseComponent<GroupBlock, GroupBlockViewModel, CreateGroupBlockViewModel, UpdateGroupBlockViewModel>
{
    // [Inject] protected GroupBlocksManager GroupBlocksManager { get; set; } = null!;
    [Inject] protected GroupBlocksDialogManager GroupBlocksDialogManager { get; set; } = null!;

    protected override void OnInitialized()
    {
        // Manager = GroupBlocksManager;
        DialogManager = GroupBlocksDialogManager;
        base.OnInitialized();
    }
}