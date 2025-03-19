using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Services;

public class EntriesManager : EntityManagerBase<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    private readonly IEntryService _service;

    public EntriesManager(IEntryService service) : base(service)
    {
        _service = service;
    }

    public async Task<Result> AcceptEntry(Guid id)
    {
        return await _service.AcceptEntry(id);
    }
}