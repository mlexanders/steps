using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Services;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Shared;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;

namespace Steps.Application.Requests.ScheduleFile.Commands;
public record CreateFinalScheduleFileCommand(CreateFinalScheduleFileViewModel Model) : IRequest<Result<ScheduleFileViewModel>>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}

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