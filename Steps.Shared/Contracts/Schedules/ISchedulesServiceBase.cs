using Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

namespace Steps.Shared.Contracts.Schedules;

public interface ISchedulesServiceBase<TScheduledCell> where TScheduledCell : ScheduledCellViewModelBase
{
    Task<Result<PaggedListViewModel<TScheduledCell>>> GetPagedScheduledCellsByGroupBlockIdQuery(GetPagedPreScheduledCellsViewModel model);
}