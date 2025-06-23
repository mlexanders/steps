using System.ComponentModel.DataAnnotations;
using Calabonga.PagedListCore;

namespace Steps.Shared.Utils;

public static class Extensions
{
    public static string GetDisplayName(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

        if (attributes != null && attributes.Length > 0) return attributes.First()!.GetName()!;

        return value.ToString();
    }

    public static PaggedListViewModel<T> GetView<T>(this IPagedList<T> list)
    {
        return new PaggedListViewModel<T>
        {
            IndexFrom = list.IndexFrom,
            PageIndex = list.PageIndex,
            PageSize = list.PageSize,
            TotalCount = list.TotalCount,
            TotalPages = list.TotalPages,
            Items = list.Items,
            HasPreviousPage = list.HasPreviousPage,
            HasNextPage = list.HasNextPage,
        };
    }
}