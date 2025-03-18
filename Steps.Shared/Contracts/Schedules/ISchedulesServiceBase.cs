namespace Steps.Shared.Contracts.Schedules;

public interface ISchedulesServiceBase<TEntity, TScheduledCellViewModel, in TGetView> 
    where TScheduledCellViewModel : ScheduledCellViewModelBase
    where TGetView : GetPagedScheduledCellsBase<TEntity>
    where TEntity : class
{
    Task<Result<PaggedListViewModel<TScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(TGetView model);
}