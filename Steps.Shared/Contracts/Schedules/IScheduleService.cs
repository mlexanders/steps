
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Shared.Contracts.Schedules;

public interface ISchedulesService
{
    Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(Guid groupBlockId, Page page);
    Task<Result> Reorder(ReorderGroupBlockViewModel model);
}

public class ReorderGroupBlockViewModel
{
    public Guid GroupBlockId { get; set; }
    public List<ScheduleAthleteViewMode> Schedule { get; set; } = null!;
}

public class ScheduleAthleteViewMode
{
    public Guid AthleteId { get; set; }
    public int SequenceNumber { get; set; }
}
    
