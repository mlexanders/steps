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
    private readonly IEntryRoutes _entryRoutes;
    public EntryService(HttpClientService httpClient, IEntryRoutes entryRoutes) : base(httpClient, new ApiRoutes.EntriesRoute())
    {
        _entryRoutes = entryRoutes;
    }

    public async Task<Result> AcceptEntry(EntryViewModel entryViewModel)
    {
        return await HttpClient.PostAsync<Result, EntryViewModel>(_entryRoutes.AcceptEntry(), entryViewModel);
    }
}