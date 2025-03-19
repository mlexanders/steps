namespace Steps.Shared.Contracts.Schedules;

public class GetPagedScheduledCellsBase<T> where T : class
{
    public Guid GroupBlockId { get; set; }
    public Page Page { get; set; } = new Page();
    public virtual Specification<T>? Specification { get; set; }
}