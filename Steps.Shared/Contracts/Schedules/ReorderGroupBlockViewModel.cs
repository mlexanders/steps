namespace Steps.Shared.Contracts.Schedules;

public class ReorderGroupBlockViewModel
{
    public Guid GroupBlockId { get; set; }
    public List<ScheduleAthleteViewModel> Schedule { get; set; } = null!;
}