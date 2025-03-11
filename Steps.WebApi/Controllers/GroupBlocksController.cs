using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.GroupBlocks.Commands;
using Steps.Application.Requests.GroupBlocks.Queries;
using Steps.Shared;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;

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

    [HttpGet("{id:guid}")]
    public Task<Result<GroupBlockViewModel>> GetById(Guid id)
    {
        return _mediator.Send(new GetGroupBlockByIdQuery(id));
    }

    [HttpDelete("{contestId:guid}")]
    public Task<Result> DeleteByContestId(Guid contestId)
    {
        return _mediator.Send(new DeleteByContestId(contestId));
    }

    [HttpGet("[action]/{contestId:guid}")]
    public Task<Result<List<TeamViewModel>>> GetTeamsForCreateGroupBlocks(Guid contestId)
    {
        return _mediator.Send(new GetTeamsForCreateGroupBlocksQuery(contestId));
    }

    [HttpPost("[action]")]
    public Task<Result> CreateByTeams([FromBody] CreateGroupBlockViewModel model)
    {
        return _mediator.Send(new CreateGroupBlocksByTeamsCommand(model));
    }

    [HttpGet("[action]/{contestId:guid}")]
    public Task<Result<List<GroupBlockViewModel>>> GetByContestId(Guid contestId)
    {
        return _mediator.Send(new GetGroupBlocksByContestIdQuery(contestId));
    }
}



