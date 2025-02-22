namespace Steps.Shared.Contracts;

public class Page
{
    public const int DefaultPageSize = 10;

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

    public Page(int skip, int take)
    {
        PageIndex = skip > 0 ? (skip / take) : 0;
        PageSize = take > 0 ? take : DefaultPageSize;
    }

    public string GetQuery()
    {
        var indexName = nameof(PageIndex);
        var sizename = nameof(PageSize);
        return $"?{indexName}={PageIndex}&{sizename}={PageSize}";
    }
}