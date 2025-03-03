namespace Steps.Filters.Filters;

public class FilterGroup
{
    public string Logic { get; set; } = "and";
    public List<FilterModel> Filters { get; set; } = [];
}