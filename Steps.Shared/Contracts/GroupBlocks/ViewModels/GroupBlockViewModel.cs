using Steps.Domain.Base;
using Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

namespace Steps.Shared.Contracts.GroupBlocks.ViewModels;

public class GroupBlockViewModel : IHaveId
{
    public Guid Id { get; set; }
    public Guid ContestId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public List<PreScheduledCellViewModel> ScheduledCells { get; set; } = [];
}