using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Schedules.Queries;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class FinalSchedulesController : ControllerBase, IFinalSchedulesService
{
    private readonly IMediator _mediator;

    public FinalSchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<FinalScheduledCellViewModel>>> GetPagedScheduledCellsByGroupBlockIdQuery(
        [FromBody] GetPagedFinalScheduledCells model)
    {
        return _mediator.Send(new GetPagedFinalScheduledCellsByGroupBlockIdQuery(model));
    }
}

