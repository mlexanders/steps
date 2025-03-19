using Steps.Domain.Base;

namespace Steps.Shared.Contracts;

public interface ICrudService<TEntity, TViewModel, in TCreateViewModel, in TUpdateViewModel> :  IPaginationService<TEntity, TViewModel>
    where TEntity : class
    where TViewModel : IHaveId
    where TCreateViewModel : class
    where TUpdateViewModel : IHaveId
{
    Task<Result<TViewModel>> Create(TCreateViewModel model);
    Task<Result<TViewModel>> GetById(Guid id);
    Task<Result<Guid>> Update(TUpdateViewModel model);
    Task<Result> Delete(Guid id);
}