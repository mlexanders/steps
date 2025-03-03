using Steps.Domain.Base;

namespace Steps.Shared.Contracts;

public interface ICrudService<TEntity, TViewModel, in TCreateViewModel, in TUpdateViewModel> 
    where TEntity : class
    where TViewModel : IHaveId
    where TCreateViewModel : class
    where TUpdateViewModel : IHaveId
{
    Task<Result<TViewModel>> Create(TCreateViewModel model);
    Task<Result<TViewModel>> GetById(Guid id);
    Task<Result<Guid>> Update(TUpdateViewModel model);
    Task<Result<PaggedListViewModel<TViewModel>>> GetPaged(Page page, Specification<TEntity>? specification = null);
    Task<Result> Delete(Guid id);
}