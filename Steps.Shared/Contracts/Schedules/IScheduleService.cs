using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Shared.Contracts.Schedules;

public interface ISchedulesService
{
    Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(GetPagedScheduledCellsViewModel model);
    Task<Result> Reorder(ReorderGroupBlockViewModel model);
    Task<Result> MarkAthlete(MarkAthleteViewModel model);
}

public class GetPagedScheduledCellsViewModel
{
    public Guid GroupBlockId { get; set; }
    public Page Page { get; set; } = new Page();
    public Specification<ScheduledCell>? Specification { get; set; }
}

public class MarkAthleteViewModel
{
    public Guid GroupBlockId { get; set; }
    public Guid AthleteId { get; set; }
    public bool Confirmation { get; set; }
}