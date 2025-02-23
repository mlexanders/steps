using Calabonga.PagedListCore;

namespace Steps.Shared;

public class PaggedListViewModel<T> : IPagedList<T>
{
    public int IndexFrom { get; set; }
    public int PageIndex { get; set;}
    public int PageSize { get; set;}
    public int TotalCount { get; set;}
    public int TotalPages { get; set;}
    public IList<T> Items { get; set; } = [];
    public bool HasPreviousPage { get; set;}
    public bool HasNextPage { get; set;}
}