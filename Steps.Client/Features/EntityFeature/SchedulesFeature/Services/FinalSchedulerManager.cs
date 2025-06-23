using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.ScheduleFile;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Client.Features.EntityFeature.SchedulesFeature.Services;

/// <summary>
/// Менеджер для Финального расписания
/// </summary>
public class FinalSchedulerManager : SchedulerManagerBase<FinalScheduledCell, FinalScheduledCellViewModel, GetPagedFinalScheduledCells>
{
    private readonly IFinalSchedulesService _service;
    private readonly IScheduleFileService _serviceScheduleFileService;

    public FinalSchedulerManager(IFinalSchedulesService service, IScheduleFileService serviceScheduleFileService) : base(service)
    {
        _service = service;
        _serviceScheduleFileService = serviceScheduleFileService;
    }
    
    public async Task<Result<ScheduleFileViewModel>> GenerateScheduleFile(CreateFinalScheduleFileViewModel model)
    {
        try
        {
            var result = await _serviceScheduleFileService.CreateScheduleFile(model);
            return result;
        }
        catch (Exception e)
        {
            return Result<ScheduleFileViewModel>.Fail(e.Message);
        }
    }
}