using Steps.Client.Features.Common.Pagination;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Client.Features.EntityFeature.Schedules.Services;

public class SchedulerManager : PaginationManagerBase<ScheduledCellViewModel>
{
    private readonly ISchedulesService _service;
    private Guid? _groupBlockId;

    public SchedulerManager(ISchedulesService service)
    {
        _service = service;
    }

    public Task Initialize(Guid groupBlockId)
    {
        _groupBlockId = groupBlockId;
        return base.Initialize();
    }
    
    protected override async Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> GetPaged()
    {
        if (_groupBlockId != null)
            return await _service.GetPagedScheduledCellsByGroupBlockIdQuery(_groupBlockId.Value, CurrentPage);
        return Result<PaggedListViewModel<ScheduledCellViewModel>>.Fail("Групповой блок не выбран");
    }

    public Task<Result> Reorder(ReorderGroupBlockViewModel model)
    {
        return _service.Reorder(model);
    }
}
