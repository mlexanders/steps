using Steps.Shared.Contracts.ScheduleFile.ViewModel;

namespace Steps.Client.Services.Api.Routes;

public interface IScheduleFileRoutes
{
    string CreatePreScheduleFile(CreatePreScheduleFileViewModel createPreScheduleFileViewModel);
}