using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Components;

public partial class EntryCard 
{
    [Inject] protected EntriesManager EntriesManager { get; set; } = null!;
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;
    
    [Parameter] public EntryViewModel Model { get; set; } = null!;
    
    [Parameter] [Required] public ContestViewModel Contest { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }

    protected async Task OnCreate()
    {
        var result = await EntriesDialogManager.ShowCreateDialog(Contest.Id);
        if (result) await EntriesManager.LoadPage();
    }
    
    protected async Task OnAccept()
    {
        var result = await EntriesManager.AcceptEntry(Model.Id);
        ShowResultMessage(result);
        await EntriesManager.LoadPage();
    }
}