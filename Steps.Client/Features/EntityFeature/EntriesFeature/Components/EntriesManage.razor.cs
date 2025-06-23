using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Components;

public partial class EntriesManage : ManageBaseComponent<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    [Inject] protected EntriesManager EntriesManagement { get; set; } = null!;
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;

    [Parameter] public bool IsReadonly { get; set; }

    private bool IsSuccess(EntryViewModel e) => e.IsSuccess == true;
    private IEnumerable<EntryViewModel> AcceptedEntries => Manager.Data?.Where(e => e.IsSuccess) ?? [];
    private IEnumerable<EntryViewModel> RejectedEntries => Manager.Data?.Where(e => !e.IsSuccess) ?? [];

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

    protected override async Task<Specification<Entry>?> GetSpecification()
    {
        return null;
    }
}