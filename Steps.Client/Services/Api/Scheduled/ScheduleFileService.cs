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
        private readonly ApiRoutes.ScheduleFilesRoute _routes;

        public ScheduleFileService(HttpClientService httpClient)
        {
            _httpClient = httpClient;
            _routes = new ApiRoutes.ScheduleFilesRoute();
        }

        public Task<Result<ScheduleFileViewModel>> CreatePreScheduleFile(CreatePreScheduleFileViewModel createPreScheduleFileViewModel)
        {
            return _httpClient.PostAsync<Result<ScheduleFileViewModel>, CreatePreScheduleFileViewModel>(
                _routes.CreatePreScheduleFile(createPreScheduleFileViewModel), createPreScheduleFileViewModel);
        }

        public Task<Result<ScheduleFileViewModel>> CreateScheduleFile(CreateFinalScheduleFileViewModel createFinalScheduleFileViewModel)
        {
            return _httpClient.PostAsync<Result<ScheduleFileViewModel>, CreateFinalScheduleFileViewModel>(
                _routes.CreateFinalScheduleFile(createFinalScheduleFileViewModel), createFinalScheduleFileViewModel);
        }
    }
}
