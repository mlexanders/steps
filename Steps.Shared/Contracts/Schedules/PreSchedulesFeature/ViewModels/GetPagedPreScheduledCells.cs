using Steps.Domain.Entities.GroupBlocks;

namespace Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

public class GetPagedPreScheduledCells : GetPagedScheduledCellsBase<PreScheduledCell>
{
    public override Specification<PreScheduledCell>? Specification { get; set; }
}

public class GetPagedFinalScheduledCells : GetPagedScheduledCellsBase<FinalScheduledCell>
{
    public override Specification<FinalScheduledCell>? Specification { get; set; }
}