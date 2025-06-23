using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Services;
using Steps.Shared;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;

namespace Steps.Application.Requests.ScheduleFile.Commands;
public record CreateFinalScheduleFileCommand(CreateFinalScheduleFileViewModel Model) : IRequest<Result<ScheduleFileViewModel>>;

public class CreateFinalScheduleFileCommandHandler : IRequestHandler<CreateFinalScheduleFileCommand, Result<ScheduleFileViewModel>>
{
    private readonly ScheduleFileService _scheduleFileService;

    public CreateFinalScheduleFileCommandHandler(ScheduleFileService scheduleFileService)
    {
        _scheduleFileService = scheduleFileService;
    }

    public async Task<Result<ScheduleFileViewModel>> Handle(CreateFinalScheduleFileCommand request, CancellationToken cancellationToken)
    {
        var scheduleFile = await _scheduleFileService.GenerateFinalScheduleFile(request.Model.GroupBlockIds);
        return Result<ScheduleFileViewModel>.Ok(scheduleFile).SetMessage("Файл сформирован");
    }
}