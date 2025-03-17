using Steps.Domain.Base;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Shared.Contracts.Schedules;

public abstract class ScheduledCellViewModelBase : IHaveId
{
    public Guid Id { get; set; }
    public DateTime ExitTime { get; set; }
    public int SequenceNumber { get; set; }
    public Guid AthleteId { get; set; }
    public AthleteViewModel Athlete { get; set; }
    public string ClubName { get; set; }
    public string TeamName { get; set; }
    public Guid GroupBlockId { get; set; }
}