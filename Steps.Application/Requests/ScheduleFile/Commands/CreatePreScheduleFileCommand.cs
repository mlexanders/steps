using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Requests.GroupBlocks.Commands;
using Steps.Application.Services;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.ScheduleFile.Commands;

public record CreatePreScheduleFileCommand(CreatePreScheduleFileViewModel Model) : IRequest<Result<ScheduleFileViewModel>>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}

public class CreatePreScheduleFileCommandHandler : IRequestHandler<CreatePreScheduleFileCommand, Result<ScheduleFileViewModel>>
{
    private readonly ScheduleFileService _scheduleFileService;

    public CreatePreScheduleFileCommandHandler(ScheduleFileService scheduleFileService)
    {
        _scheduleFileService = scheduleFileService;
    }

    public async Task<Result<ScheduleFileViewModel>> Handle(CreatePreScheduleFileCommand request, CancellationToken cancellationToken)
    {
        var scheduleFile = await _scheduleFileService.GeneratePreScheduleFile(request.Model.GroupBlockIds);
        return Result<ScheduleFileViewModel>.Ok(scheduleFile).SetMessage("Файл сформирован");
    }
}