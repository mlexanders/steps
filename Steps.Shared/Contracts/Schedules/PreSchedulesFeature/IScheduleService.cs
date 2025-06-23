using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Shared.Contracts.Schedules.PreSchedulesFeature;

public interface IPreSchedulesService : ISchedulesServiceBase<PreScheduledCell, PreScheduledCellViewModel, GetPagedPreScheduledCells>
{
    Task<Result> Reorder(ReorderGroupBlockViewModel model);
    Task<Result> MarkAthlete(MarkAthleteViewModel model);
}

