using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Dialogs;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Components;

public partial class ContestCard: ManageBaseComponent<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    [Inject] protected EntriesManagement EntriesManagement { get; set; } = null!;
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;
    [Inject] protected ContestManager ContestManager { get; set; } = null!;
    [Inject] protected ContestDialogManager ContestDialogManager { get; set; } = null!;
    
    private CreateEntryDialog _createEntryDialog;

    protected override void OnInitialized()
    {
        Manager = ContestManager;
        DialogManager = ContestDialogManager;

        base.OnInitializedAsync();
    }

    protected override async Task OnCreate()
    {
        var result = await EntriesDialogManager.ShowCreateDialog(Model.Id);
        if (result) await Manager.LoadPage();
    }
}