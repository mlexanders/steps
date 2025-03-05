using Steps.Domain.Entities;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Services;

public class EntriesManagement : BaseEntityManager<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    public EntriesManagement(IEntryService service) : base(service)
    {
    }
}