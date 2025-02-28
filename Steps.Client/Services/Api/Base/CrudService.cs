using Steps.Client.Services.Api.Routes;
using Steps.Domain.Base;
using Steps.Filters.Filters;
using Steps.Shared;
using Steps.Shared.Contracts;

namespace Steps.Client.Services.Api.Base;

public abstract class CrudService<TViewModel, TCreateViewModel, TUpdateViewModel>
    : ICrudService<TViewModel, TCreateViewModel, TUpdateViewModel>
    where TViewModel : IHaveId
    where TCreateViewModel : class, new()
    where TUpdateViewModel : class, IHaveId, new()
{
    protected readonly HttpClientService _httpClient;
    protected readonly IApiRoutes ApiRoutes;

    protected CrudService(HttpClientService httpClient, IApiRoutes apiRoutes)
    {
        _httpClient = httpClient;
        ApiRoutes = apiRoutes;
    }

    public Task<Result<TViewModel>> Create(TCreateViewModel model)
    {
        return _httpClient.PostAsync<Result<TViewModel>, TCreateViewModel>(ApiRoutes.Create(),
            model);
    }

    public Task<Result<List<TViewModel>>> GetBy(FilterGroup filter)
    {
        return _httpClient.PostAsync<Result<List<TViewModel>>, FilterGroup>(ApiRoutes.GetBy(), filter);
    }

    public Task<Result<Guid>> Update(TUpdateViewModel model)
    {
        return _httpClient.PatchAsync<Result<Guid>, TUpdateViewModel>(ApiRoutes.Update(),
            model);
    }

    public Task<Result<TViewModel>> GetById(Guid id)
    {
        var route = ApiRoutes.GetById(id);
        return _httpClient.GetAsync<Result<TViewModel>>(route);
    }

    public Task<Result<PaggedListViewModel<TViewModel>>> GetPaged(Page page)
    {
        var route = ApiRoutes.GetPaged(page);
        return _httpClient.GetAsync<Result<PaggedListViewModel<TViewModel>>>(route);
    }

    public Task<Result> Delete(Guid id)
    {
        var route = ApiRoutes.Delete(id);
        return _httpClient.DeleteAsync<Result>(route);
    }
}