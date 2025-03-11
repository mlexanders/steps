using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Schedules.Command;
using Steps.Application.Requests.Schedules.Queries;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class SchedulesController : ControllerBase, ISchedulesService
{
    private readonly IMediator _mediator;

    public SchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]/{groupBlockId:guid}")]
    public Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> 
        GetPagedScheduledCellsByGroupBlockIdQuery(Guid groupBlockId, [FromBody] Page page)
    {
        return _mediator.Send(new GetPagedScheduledCellsByGroupBlockIdQuery(groupBlockId, page));
    }

    [HttpPost("[action]")]
    public Task<Result> Reorder([FromBody] ReorderGroupBlockViewModel model)
    {
        return _mediator.Send(new ReorderGroupBlockCommand(model));
    }
}