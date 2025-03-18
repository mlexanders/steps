using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Client.Services.Api.Scheduled;

/// <summary>
/// HTTP сервис для финальных блоков
/// </summary>
public class FinalSchedulesService : SchedulesServiceBase<FinalScheduledCell, FinalScheduledCellViewModel, GetPagedFinalScheduledCells>,
    IFinalSchedulesService
{
    public FinalSchedulesService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.FinalSchedulesRoute())
    {
    }
}