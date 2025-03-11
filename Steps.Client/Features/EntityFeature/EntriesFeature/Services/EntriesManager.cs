using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Services;

public class EntriesManager : EntityManagerBase<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    public EntriesManager(IEntryService service) : base(service)
    {
    }
}