using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Requests.GroupBlocks.Commands;
using Steps.Application.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.ScheduleFile.Commands;

public record CreatePreScheduleFileCommand(CreatePreScheduleFileViewModel Model) : IRequest<Result>;

public class CreatePreScheduleFileCommandHandler : IRequestHandler<CreatePreScheduleFileCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ScheduleFileService _scheduleFileService;

    public CreatePreScheduleFileCommandHandler(IUnitOfWork unitOfWork,
        ScheduleFileService scheduleFileService)
    {
        _unitOfWork = unitOfWork;
        _scheduleFileService = scheduleFileService;
    }

    public async Task<Result> Handle(CreatePreScheduleFileCommand request,
        CancellationToken cancellationToken)
    {
        
        
        return Result.Ok().SetMessage("Файл сформирован");
    }
}