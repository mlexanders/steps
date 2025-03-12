using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.AthleteFeature.Dialogs;
using Steps.Client.Features.EntityFeature.AthleteFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Client.Features.EntityFeature.TeamsFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Components;

public partial class EntryCard: ManageBaseComponent<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    [Inject] protected EntriesManager EntriesManagement { get; set; } = null!;
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;
    
    [Parameter] [Required] public ContestViewModel Contest { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }

    protected override void OnInitialized()
    {
        Manager = EntriesManagement;
        DialogManager = EntriesDialogManager;

        base.OnInitialized();
    }

    protected override async Task OnCreate()
    {
        var result = await EntriesDialogManager.ShowCreateDialog(Contest.Id);
        if (result) await Manager.LoadPage();
    }
    
    protected async Task OnAccept()
    {
        var result = await EntriesDialogManager.ShowCreateDialog(Contest.Id);
        if (result) await Manager.LoadPage();
    }
}