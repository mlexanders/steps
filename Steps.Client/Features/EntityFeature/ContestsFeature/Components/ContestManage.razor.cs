using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Client.Features.EntityFeature.ContestsFeature.Dialogs;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Components;

public partial class
    ContestManage : ManageBaseComponent<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    [Inject] protected ContestManager ContestManager { get; set; } = null!;
    [Inject] protected ContestDialogManager ContestDialogManager { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        Manager = ContestManager;
        DialogManager = ContestDialogManager;

        base.OnInitialized();
    }
}