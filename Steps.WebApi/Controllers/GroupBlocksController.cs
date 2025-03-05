using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.GroupBlocks.Commands;
using Steps.Application.Requests.GroupBlocks.Queries;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class GroupBlocksController : ControllerBase, IGroupBlocksService
{
    private readonly IMediator _mediator;

    public GroupBlocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Result<GroupBlockViewModel>> Create([FromBody] CreateGroupBlockViewModel model)
    {
        return _mediator.Send(new CreateGroupBlockCommand(model));
    }

    [HttpGet("{id:guid}")]
    public Task<Result<GroupBlockViewModel>> GetById(Guid id)
    {
        return _mediator.Send(new GetGroupBlockByIdQuery(id));
    }

    [HttpPatch]
    public Task<Result<Guid>> Update([FromBody] UpdateGroupBlockViewModel model)
    {
        return _mediator.Send(new UpdateGroupBlock(model));
    }

    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<GroupBlockViewModel>>> GetPaged([FromQuery] Page page,
        [FromBody] Specification<GroupBlock>? specification = null)
    {
        return _mediator.Send(new GetPagedGroupBlocksQuery(page, specification));
    }

    [HttpDelete("{id:guid}")]
    public Task<Result> Delete(Guid id)
    {
        return _mediator.Send(new DeleteGroupBlock(id));
    }
}