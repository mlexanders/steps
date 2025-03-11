namespace Steps.Shared.Contracts.Schedules;

public class ReorderGroupBlockViewModel
{
    public Guid GroupBlockId { get; set; }
    public List<ScheduleAthleteViewMode> Schedule { get; set; } = null!;
}