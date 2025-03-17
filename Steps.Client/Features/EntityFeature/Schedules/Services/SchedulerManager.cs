using Steps.Client.Features.Common.Pagination;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedules;
using Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

namespace Steps.Client.Features.EntityFeature.Schedules.Services;

public class SchedulerManager : PaginationManagerBase<PreScheduledCellViewModel>
{
    private readonly ISchedulesService _service;
    private Guid? _groupBlockId;
    private Specification<PreScheduledCell>? _specification;

    public SchedulerManager(ISchedulesService service)
    {
        _service = service;
    }

    public Task Initialize(Guid groupBlockId)
    {
        _groupBlockId = groupBlockId;
        return base.Initialize();
    }
    
    public virtual void UseSpecification(Specification<PreScheduledCell> specification)
    {
        _specification = specification;
    }
    
    protected override async Task<Result<PaggedListViewModel<PreScheduledCellViewModel>>> GetPaged()
    {
        if (_groupBlockId != null)
            return await _service.GetPagedScheduledCellsByGroupBlockIdQuery(new GetPagedPreScheduledCellsViewModel
            {
                GroupBlockId = _groupBlockId.Value,
                Page = CurrentPage,
                Specification = _specification,
            });
        return Result<PaggedListViewModel<PreScheduledCellViewModel>>.Fail("Групповой блок не выбран");
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
