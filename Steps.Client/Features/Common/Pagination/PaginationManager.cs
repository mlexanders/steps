using Steps.Shared;
using Steps.Shared.Contracts;

namespace Steps.Client.Features.Common.Pagination;

public abstract class PaginationManagerBase<TViewModel>
{
    public event Action? ChangedList;

    public bool IsLoading { get; private set; }

    public int TotalCount { get; private set; }

    public List<TViewModel> Data { get; private set; } = [];

    public int PageSize => CurrentPage.PageSize;

    protected Page CurrentPage { get; private set; } = new(0, Page.DefaultPageSize);

    public virtual async Task Initialize()
    {
        await LoadPage();
    }

    public async Task ChangePage(int? skip, int? take)
    {
        if (skip != null && take != null)
            CurrentPage = new Page(true, skip.Value, take.Value);
        await LoadPage();
    }

    public void ChangePageSize(int pageSize)
    {
        var newPageSize = pageSize > 0 ? pageSize : Page.DefaultPageSize;
        CurrentPage = new Page(0, newPageSize);
    }

    protected virtual void HandleException(Exception exception)
    {
        //TODO:
        Console.WriteLine(exception);
    }

    public async Task LoadPage()
    {
        IsLoading = true;
        try
        {
            var result = await GetPaged();
            if (result?.IsSuccess != true) return;

            var pagedList = result.Value;
            Data = pagedList?.Items.ToList() ?? [];
            TotalCount = pagedList?.TotalCount ?? 0;

            OnChangedList();
        }
        catch (Exception e)
        {
            HandleException(e);
        }

        IsLoading = false;
    }

    protected abstract Task<Result<PaggedListViewModel<TViewModel>>> GetPaged();

    private void OnChangedList()
    {
        ChangedList?.Invoke();
    }
}