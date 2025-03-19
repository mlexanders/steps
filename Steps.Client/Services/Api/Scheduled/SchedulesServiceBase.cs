using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules;

namespace Steps.Client.Services.Api.Scheduled;

/// <summary>
/// Базовый сервис HTTP для расписаний
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TScheduledCellViewModel"></typeparam>
/// <typeparam name="TGetView"></typeparam>
public abstract class SchedulesServiceBase<TEntity, TScheduledCellViewModel, TGetView> : ISchedulesServiceBase<TEntity, TScheduledCellViewModel, TGetView> 
    where TScheduledCellViewModel : ScheduledCellViewModelBase
    where TGetView : GetPagedScheduledCellsBase<TEntity>, new()
    where TEntity : class
{
    private readonly HttpClientService _httpClient;
    private readonly ApiRoutes.ScheduledRoutes _routes;

    protected SchedulesServiceBase(HttpClientService httpClient, ApiRoutes.ScheduledRoutes routes)
    {
        _httpClient = httpClient;
        _routes = routes;
    }

    public Task<Result<PaggedListViewModel<TScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(TGetView model)
    {
        return _httpClient.PostAsync<Result<PaggedListViewModel<TScheduledCellViewModel>>, TGetView>(
            _routes.GetPagedScheduled, model);
    }
}