using Steps.Shared;
using Steps.Shared.Contracts;

namespace Steps.Client.Features.Common.Pagination;


public abstract class DefaultPaginationManagerBase<TEntity, TViewModel> : PaginationManagerBase<TViewModel>
    where TEntity : class
{
    private readonly IPaginationService<TEntity, TViewModel> _service;
    private Specification<TEntity>? _specification;

    protected DefaultPaginationManagerBase(IPaginationService<TEntity, TViewModel> service)
    {
        _service = service;
    }

    public virtual void UseSpecification(Specification<TEntity> specification)
    {
        _specification = specification;
    }
    
    protected override Task<Result<PaggedListViewModel<TViewModel>>> GetPaged()
    {
        return _service.GetPaged(CurrentPage, _specification);
    }
}