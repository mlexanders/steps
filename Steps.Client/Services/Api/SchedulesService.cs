using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Client.Services.Api;

public class SchedulesService : ISchedulesService
{
    private readonly HttpClientService _httpClient;
    private readonly ApiRoutes.SchedulesService _routes;

    public SchedulesService(HttpClientService httpClient)
    {
        _httpClient = httpClient;
        _routes = new ApiRoutes.SchedulesService();
    }

    public Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(Guid groupBlockId, Page page)
    {
        return _httpClient.PostAsync<Result<PaggedListViewModel<ScheduledCellViewModel>>, Page>(
            _routes.GetPagedScheduledCellsByGroupBlockIdQuery(groupBlockId), page);
    }

    public Task<Result> Reorder(ReorderGroupBlockViewModel model)
    {
        return _httpClient.PostAsync<Result, ReorderGroupBlockViewModel>(_routes.Reorder, model);
    }
}