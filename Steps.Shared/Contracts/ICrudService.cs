using Steps.Domain.Base;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts;

public interface ICrudService<TViewModel, in TCreateViewModel, in TUpdateViewModel> 
    where TViewModel : IHaveId
    where TCreateViewModel : class
    where TUpdateViewModel : IHaveId
{
    Task<Result<TViewModel>> Create(TCreateViewModel model);
    Task<Result<Guid>> Update(TUpdateViewModel model);
    Task<Result<TViewModel>> GetById(Guid id);
    Task<Result<PaggedListViewModel<TViewModel>>> GetPaged(Page page);
    Task<Result> Delete(Guid id);
}