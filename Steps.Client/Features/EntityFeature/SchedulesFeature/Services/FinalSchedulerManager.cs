using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Client.Features.EntityFeature.SchedulesFeature.Services;

/// <summary>
/// Менеджер для Финального расписания
/// </summary>
public class FinalSchedulerManager(IFinalSchedulesService service)
    : SchedulerManagerBase<FinalScheduledCell, FinalScheduledCellViewModel, GetPagedFinalScheduledCells>(service);
