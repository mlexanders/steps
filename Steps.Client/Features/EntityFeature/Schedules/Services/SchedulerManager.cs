using Steps.Client.Features.Common.Pagination;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Client.Features.EntityFeature.Schedules.Services;

public class SchedulerManager : PaginationManagerBase<ScheduledCellViewModel>
{
    private readonly ISchedulesService _service;
    private Guid? _groupBlockId;
    private Specification<ScheduledCell>? _specification;

    public SchedulerManager(ISchedulesService service)
    {
        _service = service;
    }

    public Task Initialize(Guid groupBlockId)
    {
        _groupBlockId = groupBlockId;
        return base.Initialize();
    }
    
    public virtual void UseSpecification(Specification<ScheduledCell> specification)
    {
        _specification = specification;
    }
    
    protected override async Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> GetPaged()
    {
        if (_groupBlockId != null)
            return await _service.GetPagedScheduledCellsByGroupBlockIdQuery(new GetPagedScheduledCellsViewModel
            {
                GroupBlockId = _groupBlockId.Value,
                Page = CurrentPage,
                Specification = _specification,
            });
        return Result<PaggedListViewModel<ScheduledCellViewModel>>.Fail("Групповой блок не выбран");
    }

    public Task<Result> Reorder(ReorderGroupBlockViewModel model)
    {
        return _service.Reorder(model);
    }

    public async Task<Result> MarkAthlete(MarkAthleteViewModel model)
    {
        try
        {
            var result = await _service.MarkAthlete(model);
            return result;
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}
