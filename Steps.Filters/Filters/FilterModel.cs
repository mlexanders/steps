namespace Steps.Filters.Filters;

public class FilterModel
{
    public string Param { get; set; } = string.Empty;
    public string Op { get; set; } = "eq"; // eq, neq, lt, gt, etc.
    public string Val { get; set; } = string.Empty;
}