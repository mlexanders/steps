using Steps.Shared.Contracts.ScheduleFile.ViewModel;

namespace Steps.Shared.Contracts.ScheduleFile;

public interface IScheduleFileService
{
    /// <summary>
    /// Создание предварительного списка спортсменов
    /// </summary>
    /// <param name="createPreScheduleFileViewModel"></param>
    /// <returns></returns>
    Task<Result<ScheduleFileViewModel>> CreatePreScheduleFile(CreatePreScheduleFileViewModel createPreScheduleFileViewModel);
    
    /// <summary>
    /// Создание окончательного списка спортсменов
    /// </summary>
    /// <param name="createScheduleFileViewModel"></param>
    /// <returns></returns>
    Task<Result> CreateScheduleFile(CreateScheduleFileViewModel createScheduleFileViewModel);
}