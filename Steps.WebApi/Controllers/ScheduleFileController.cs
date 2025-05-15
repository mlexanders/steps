using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Entries.Commands;
using Steps.Application.Requests.ScheduleFile.Commands;
using Steps.Shared;
using Steps.Shared.Contracts.Entries.ViewModels;
using Steps.Shared.Contracts.ScheduleFile;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class ScheduleFileController : ControllerBase, IScheduleFileService
{
    private readonly IMediator _mediator;

    public ScheduleFileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<Result> CreatePreScheduleFile(CreatePreScheduleFileViewModel createPreScheduleFileViewModel)
    {
        return await _mediator.Send(new CreatePreScheduleFileCommand(createPreScheduleFileViewModel));
    }
    
    [HttpPost("[action]")]
    public async Task<Result> CreateScheduleFile(CreateScheduleFileViewModel createScheduleFileViewModel)
    {
        throw new NotImplementedException();
    }
}