using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedules;
using Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

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

    public Task<Result<PaggedListViewModel<PreScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(GetPagedPreScheduledCellsViewModel model)
    {
        return _httpClient.PostAsync<Result<PaggedListViewModel<PreScheduledCellViewModel>>, GetPagedPreScheduledCellsViewModel>(
            _routes.GetPagedScheduledCellsByGroupBlockIdQuery, model);
    }

    public Task<Result> Reorder(ReorderGroupBlockViewModel model)
    {
        return _httpClient.PostAsync<Result, ReorderGroupBlockViewModel>(_routes.Reorder, model);
    }

    public Task<Result> MarkAthlete(MarkAthleteViewModel model)
    {
        return _httpClient.PostAsync<Result, MarkAthleteViewModel>(_routes.MarkAthlete, model);
    }
}