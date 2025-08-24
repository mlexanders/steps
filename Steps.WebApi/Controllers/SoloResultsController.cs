using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.SoloResults.Commands;
using Steps.Application.Requests.SoloResults.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class SoloResultsController : ControllerBase, ISoloResultsService
{
    private readonly IMediator _mediator;

    public SoloResultsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Result<SoloResultViewModel>> Create([FromBody] CreateSoloResultViewModel model)
    {
        return _mediator.Send(new CreateSoloResultCommand(model));
    }

    [HttpGet("{id:guid}")]
    public Task<Result<SoloResultViewModel>> GetById(Guid id)
    {
        return _mediator.Send(new GetSoloResultByIdQuery(id));
    }

    [HttpPatch]
    public Task<Result<Guid>> Update([FromBody] UpdateSoloResultViewModel model)
    {
        return _mediator.Send(new UpdateSoloResultCommand(model));
    }

    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<SoloResultViewModel>>> GetPaged([FromQuery] Page page,
        [FromBody] Specification<SoloResult>? specification = null)
    {
        return _mediator.Send(new GetPaggedSoloResultQuery(page, specification));
    }

    [HttpDelete("{id:guid}")]
    public Task<Result> Delete(Guid id)
    {
        return _mediator.Send(new DeleteSoloResultCommand(id));
    }
}
