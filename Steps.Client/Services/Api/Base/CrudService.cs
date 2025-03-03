using Steps.Client.Services.Api.Routes;
using Steps.Domain.Base;
using Steps.Shared;
using Steps.Shared.Contracts;

namespace Steps.Client.Services.Api.Base;

public abstract class CrudService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel>
    : ICrudService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel>
    where TViewModel : IHaveId
    where TCreateViewModel : class, new()
    where TUpdateViewModel : class, IHaveId, new()
    where TEntity : class, IHaveId
{
    protected readonly HttpClientService HttpClient;
    protected readonly IApiRoutes ApiRoutes;

    protected CrudService(HttpClientService httpClient, IApiRoutes apiRoutes)
    {
        HttpClient = httpClient;
        ApiRoutes = apiRoutes;
    }

    public Task<Result<TViewModel>> Create(TCreateViewModel model)
    {
        return HttpClient.PostAsync<Result<TViewModel>, TCreateViewModel>(ApiRoutes.Create(), model);
    }

    public Task<Result<Guid>> Update(TUpdateViewModel model)
    {
        return HttpClient.PatchAsync<Result<Guid>, TUpdateViewModel>(ApiRoutes.Update(), model);
    }

    public Task<Result<TViewModel>> GetById(Guid id)
    {
        var route = ApiRoutes.GetById(id);
        return HttpClient.GetAsync<Result<TViewModel>>(route);
    }

    public Task<Result<PaggedListViewModel<TViewModel>>> GetPaged(Page page,
        Specification<TEntity>? specification = null)
    {
        var route = ApiRoutes.GetPaged(page);
        return HttpClient.PostAsync<Result<PaggedListViewModel<TViewModel>>, Specification<TEntity>> (route, specification);
    }

    public Task<Result> Delete(Guid id)
    {
        var route = ApiRoutes.Delete(id);
        return HttpClient.DeleteAsync<Result>(route);
    }
}