using Steps.Domain.Entities;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Shared.Contracts.Entries;

public interface IEntryService : ICrudService<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>
{
    Task<Result> AcceptEntry(EntryViewModel entryViewModel);
} 