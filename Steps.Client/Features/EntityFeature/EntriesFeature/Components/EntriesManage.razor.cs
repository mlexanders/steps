using Microsoft.AspNetCore.Components;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Components;

public partial class EntriesManage : ManageBaseComponent<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    [Inject] protected EntriesManagement EntriesManagement { get; set; } = null!;
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }

    protected override void OnInitialized()
    {
        try
        {
            Manager = EntriesManagement;
            DialogManager = EntriesDialogManager;


            base.OnInitialized();
        }
        catch (Exception ex) { }
    }
}