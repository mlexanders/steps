using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.PreSchedules.Command;
using Steps.Application.Requests.PreSchedules.Queries;
using Steps.Application.Requests.Schedules.Queries;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class PreSchedulesController : ControllerBase, IPreSchedulesService
{
    private readonly IMediator _mediator;

    public PreSchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<PreScheduledCellViewModel>>>
        GetPagedScheduledCellsByGroupBlockIdQuery([FromBody] GetPagedPreScheduledCells model)
    {
        return _mediator.Send(new GetPagedPreScheduledCellsByGroupBlockIdQuery(model));
    }

    [HttpPost("[action]")]
    public Task<Result> Reorder([FromBody] ReorderGroupBlockViewModel model)
    {
        return _mediator.Send(new ReorderGroupBlockCommand(model));
    }

    [HttpPost("[action]")]
    public Task<Result> MarkAthlete(MarkAthleteViewModel model)
    {
        return _mediator.Send(new MarkAthleteCommand(model));
    }
}