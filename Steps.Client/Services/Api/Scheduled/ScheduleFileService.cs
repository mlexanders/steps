using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared;
using Steps.Shared.Contracts.ScheduleFile;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;

namespace Steps.Client.Services.Api.Scheduled
{
    public class ScheduleFileService : IScheduleFileService
    {
        private readonly HttpClientService _httpClient;
        private readonly ApiRoutes.PreSchedulesRoute _routes;

        public ScheduleFileService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.PreSchedulesRoute())
        {
            _httpClient = httpClient;
            _routes = new ApiRoutes.PreSchedulesRoute();
        }

        public Task<Result<ScheduleFileViewModel>> CreatePreScheduleFile(CreatePreScheduleFileViewModel createPreScheduleFileViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CreateScheduleFile(CreateScheduleFileViewModel createScheduleFileViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
