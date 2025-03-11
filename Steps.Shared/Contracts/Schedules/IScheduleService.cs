using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Shared.Contracts.Schedules;

public interface ISchedulesService
{
    Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(Guid groupBlockId, Page page);
    Task<Result> Reorder(ReorderGroupBlockViewModel model);
}