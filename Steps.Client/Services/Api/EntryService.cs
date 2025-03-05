using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Services.Api;

public class EntryService : CrudService<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>, IEntryService
{
    public EntryService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.EntriesRoute())
    {
    }

    public Task<Result> AcceptEntry(Guid entryId)
    {
        throw new NotImplementedException();
    }
}