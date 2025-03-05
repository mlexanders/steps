using Steps.Domain.Base;

namespace Steps.Shared.Contracts.Schedules.ViewModels;

public class GroupBlockCellViewModel : IHaveId
{
    public Guid Id { get; set; }
    public DateTime ExitTime { get; set; }
    public int SequenceNumber { get; set; }

    public Guid AthleteId { get; set; }
    public Guid GroupBlockId { get; set; }
}