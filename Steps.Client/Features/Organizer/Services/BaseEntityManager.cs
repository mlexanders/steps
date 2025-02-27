using Steps.Domain.Base;
using Steps.Shared;
using Steps.Shared.Contracts;

namespace Steps.Client.Features.Organizer.Services;

public abstract class BaseEntityManager<TViewModel, TCreateViewModel, TUpdateViewModel>
    where TViewModel : IHaveId
    where TCreateViewModel : class
    where TUpdateViewModel : IHaveId
{
    private readonly ICrudService<TViewModel, TCreateViewModel, TUpdateViewModel> _service;
    private Page _currentPage;

    protected BaseEntityManager(ICrudService<TViewModel, TCreateViewModel, TUpdateViewModel> service)
    {
        _service = service;
        PageSize = Page.DefaultPageSize;
        _currentPage = new Page(0, PageSize);
        Data = [];
        TotalCount = 0;
    }

    public event Action? ChangedList;
    public bool IsLoading { get; private set; }

    public int TotalCount { get; private set; }

    public int PageSize { get; private set; }

    public List<TViewModel> Data { get; private set; }


    public virtual async Task Initialize()
    {
        await LoadPage();
    }

    public async Task ChangePage(int? skip, int? take)
    {
        if (skip != null && take != null)
            _currentPage = new Page(true, skip.Value, take.Value);
        await LoadPage();
    }

    public void ChangePageSize(int pageSize)
    {
        PageSize = pageSize > 0 ? pageSize : Page.DefaultPageSize;
    }
    
    public virtual async Task<Result<TViewModel>> Create(TCreateViewModel model)
    {
        try
        {
            return await _service.Create(model);
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

    protected virtual void HandleException(Exception exception)
    {
        //TODO:
        Console.WriteLine(exception);
    }

    private async Task LoadPage()
    {
        IsLoading = true;
        try
        {
            await GetPage();
        }
        catch (Exception e)
        {
            HandleException(e);
        }

        IsLoading = false;
    }

    private async Task GetPage()
    {
        var result = await _service.GetPaged(_currentPage);

        if (result?.IsSuccess != true) return;

        var pagedList = result.Value;
        Data = pagedList?.Items.ToList() ?? [];
        TotalCount = pagedList?.TotalCount ?? 0;

        OnChangedList();
    }

    private void OnChangedList()
    {
        ChangedList?.Invoke();
    }
}