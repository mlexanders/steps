using Steps.Domain.Base;

namespace Steps.Shared.Contracts.Schedules.ViewModels;

public class ScheduledCellViewModel : IHaveId
{
    public Guid Id { get; set; }
    public DateTime ExitTime { get; set; }
    public int SequenceNumber { get; set; }
    public Guid AthleteId { get; set; }
    public string AthleteFullName { get; set; } = null!;
    // public Athlete Athlete { get; set; } = null!;

    public Guid GroupBlockId { get; set; }
}