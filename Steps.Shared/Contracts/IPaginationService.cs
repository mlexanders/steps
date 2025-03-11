namespace Steps.Shared.Contracts;

public interface IPaginationService<TEntity, TViewModel>
    where TEntity : class
{
    Task<Result<PaggedListViewModel<TViewModel>>> GetPaged(Page page, Specification<TEntity>? specification = null);
}