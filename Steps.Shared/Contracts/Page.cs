namespace Steps.Shared.Contracts;

public class Page
{
    private const int DefaultPageSize = 10;

    public int PageIndex { get; init; }

    public int PageSize { get; init; }

    public Page(int pageIndex, int? pageSize = null)
    {
        PageIndex = pageIndex;
        PageSize = pageSize ??  DefaultPageSize;
    }

    public Page() : this(0)
    {
    }
}