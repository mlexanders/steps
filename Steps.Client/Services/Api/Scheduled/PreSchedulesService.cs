using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Client.Services.Api.Scheduled;

/// <summary>
/// HTTP сервис для предварительных блоков
/// </summary>
public class PreSchedulesService : SchedulesServiceBase<PreScheduledCell, PreScheduledCellViewModel, GetPagedPreScheduledCells>, 
    IPreSchedulesService
{
    private readonly HttpClientService _httpClient;
    private readonly ApiRoutes.PreSchedulesRoute _routes;

    public PreSchedulesService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.PreSchedulesRoute())
    {
        _httpClient = httpClient;
        _routes = new ApiRoutes.PreSchedulesRoute();
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