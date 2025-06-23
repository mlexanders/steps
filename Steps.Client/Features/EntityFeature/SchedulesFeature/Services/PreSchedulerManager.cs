using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.ScheduleFile;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Client.Features.EntityFeature.SchedulesFeature.Services;

/// <summary>
/// Менеджер для предварительного расписания
/// </summary>
public class PreSchedulerManager : SchedulerManagerBase<PreScheduledCell, PreScheduledCellViewModel, GetPagedPreScheduledCells>
{
    private readonly IPreSchedulesService _service;
    private readonly IScheduleFileService _serviceScheduleFileService;

    public PreSchedulerManager(IPreSchedulesService service, IScheduleFileService serviceScheduleFileService) : base(service)
    {
        _service = service;
        _serviceScheduleFileService = serviceScheduleFileService;
    }

    /// <summary>
    /// Смена порядка спортсменов
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
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

    public async Task<Result<ScheduleFileViewModel>> GeneratePreScheduleFile(CreatePreScheduleFileViewModel model)
    {
        try
        {
            var result = await _serviceScheduleFileService.CreatePreScheduleFile(model);
            return result;
        }
        catch (Exception e)
        {
            return Result<ScheduleFileViewModel>.Fail(e.Message);
        }
    }
}