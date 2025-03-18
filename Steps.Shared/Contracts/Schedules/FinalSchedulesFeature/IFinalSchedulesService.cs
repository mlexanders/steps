using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Shared.Contracts.Schedules.FinalSchedulesFeature;

public interface IFinalSchedulesService : ISchedulesServiceBase<FinalScheduledCell, FinalScheduledCellViewModel, GetPagedFinalScheduledCells>;
