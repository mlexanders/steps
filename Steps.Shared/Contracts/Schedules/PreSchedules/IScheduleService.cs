using Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

namespace Steps.Shared.Contracts.Schedules.PreSchedules;

public interface ISchedulesService : ISchedulesServiceBase<PreScheduledCellViewModel>
{
    Task<Result> Reorder(ReorderGroupBlockViewModel model);
    Task<Result> MarkAthlete(MarkAthleteViewModel model);
}