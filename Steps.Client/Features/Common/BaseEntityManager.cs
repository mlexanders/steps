using Steps.Client.Features.Common.Pagination;
using Steps.Domain.Base;
using Steps.Shared;
using Steps.Shared.Contracts;

namespace Steps.Client.Features.Common;

public abstract class EntityManagerBase<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel> : DefaultPaginationManagerBase<TEntity, TViewModel>
    where TViewModel : IHaveId
    where TCreateViewModel : class
    where TUpdateViewModel : IHaveId
    where TEntity : class
{
    private readonly ICrudService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel> _service;

    protected EntityManagerBase(ICrudService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel> service) : base(service)
    {
        _service = service;
    }

    public virtual async Task<Result<TViewModel>> Create(TCreateViewModel model)
    {
        try
        {
            var result = await _service.Create(model);
            if (result.IsSuccess) OnChangedList();
            return result;
        }
        catch (Exception e)
        {
            return Result<TViewModel>.Fail(e.Message);
        }
    }

    public virtual async Task<Result<TViewModel>> Read(Guid id)
    {
        try
        {
            return await _service.GetById(id);
        }
        catch (Exception e)
        {
            return Result<TViewModel>.Fail(e.Message);
        }
    }

    public virtual async Task<Result<PaggedListViewModel<TViewModel>>> Read(Specification<TEntity> specification)
    {
        try
        {
            return await _service.GetPaged(CurrentPage, specification);
        }
        catch (Exception e)
        {
            return Result<PaggedListViewModel<TViewModel>>.Fail(e.Message);
        }
    }

    public virtual async Task<Result<Guid>> Update(TUpdateViewModel model)
    {
        try
        {
            return await _service.Update(model);
        }
        catch (Exception e)
        {
            return Result<Guid>.Fail(e.Message);
        }
    }

    public virtual async Task<Result> Delete(TViewModel model)
    {
        try
        {
            return await _service.Delete(model.Id);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}