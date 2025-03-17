using Steps.Domain.Entities.GroupBlocks;

namespace Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

public class GetPagedPreScheduledCellsViewModel : GetPagedScheduledCellsBase<PreScheduledCell>
{
    public override Specification<PreScheduledCell>? Specification { get; set; }
}