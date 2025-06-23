using Steps.Client.Features.Common.Pagination;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules;

namespace Steps.Client.Features.EntityFeature.SchedulesFeature;

/// <summary>
/// Базовый сервис с поддержкой пагинации и спецификации для моделей расписания PreScheduled / FinalScheduled 
/// </summary>
/// <typeparam name="TCell"></typeparam>
/// <typeparam name="TScheduledCellViewModel"></typeparam>
/// <typeparam name="TGetView"></typeparam>
public abstract class SchedulerManagerBase<TCell, TScheduledCellViewModel, TGetView> : PaginationManagerBase<TScheduledCellViewModel>
    where TCell : class
    where TScheduledCellViewModel : ScheduledCellViewModelBase
    where TGetView : GetPagedScheduledCellsBase<TCell>, new()
{
    private readonly ISchedulesServiceBase<TCell, TScheduledCellViewModel, TGetView> _service;
    private Guid? _groupBlockId;
    private Specification<TCell>? _specification;

    protected SchedulerManagerBase(ISchedulesServiceBase<TCell, TScheduledCellViewModel, TGetView> service)
    {
        _service = service;
    }

    public Task Initialize(Guid groupBlockId)
    {
        _groupBlockId = groupBlockId;
        return base.Initialize();
    }

    public void UseSpecification(Specification<TCell> specification)
    {
        _specification = specification;
    }

    protected override async Task<Result<PaggedListViewModel<TScheduledCellViewModel>>> GetPaged()
    {
        if (_groupBlockId != null)
            return await _service.GetPagedScheduledCellsByGroupBlockIdQuery(new TGetView()
            {
                GroupBlockId = _groupBlockId.Value,
                Page = CurrentPage,
                Specification = _specification,
            });
        return Result<PaggedListViewModel<TScheduledCellViewModel>>.Fail("Групповой блок не выбран");
    }
    
}