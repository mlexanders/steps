using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Shared.Contracts.Entries;

public interface IEntryService
{
    Task<Result<Guid>> Create(CreateEntryViewModel createEntryViewModel);
    Task<Result> AcceptEntry(Guid entryId);
} 