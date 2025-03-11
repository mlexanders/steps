using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Components;

public partial class EntryCard: ManageBaseComponent<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    [Inject] protected EntriesManager EntriesManager { get; set; } = null!;
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;
    
    [Parameter] [Required] public ContestViewModel Contest { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }

    protected override Task OnInitializedAsync()
    {
        Manager = EntriesManager;
        DialogManager = EntriesDialogManager;

        return base.OnInitializedAsync();
    }

    protected override async Task OnCreate()
    {
        var result = await EntriesDialogManager.ShowCreateDialog(Contest.Id);
        if (result) await Manager.LoadPage();
    }
}