using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Teams.Commands;
using Steps.Application.Requests.Teams.Queries;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class TeamsController : ControllerBase, ITeamsService
{
    private readonly IMediator _mediator;

    public TeamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result<TeamViewModel>> Create([FromBody] CreateTeamViewModel model)
    {
        return await _mediator.Send(new CreateTeamCommand(model));
    }

    [HttpPatch]
    public async Task<Result<Guid>> Update([FromBody] UpdateTeamViewModel model)
    {
        return await _mediator.Send(new UpdateTeamCommand(model));
    }

    [HttpGet("{teamId:guid}")]
    public async Task<Result<TeamViewModel>> GetById(Guid teamId)
    {
        return await _mediator.Send(new GetTeamByIdQuery(teamId));
    }

    [HttpGet]
    public async Task<Result<PaggedListViewModel<TeamViewModel>>> GetPaged([FromQuery] Page page)
    {
        return await _mediator.Send(new GetPagedTeamsQuery(page));
    }

    [HttpDelete("{teamId:guid}")]
    public async Task<Result> Delete(Guid teamId)
    {
        return await _mediator.Send(new DeleteTeamCommand(teamId));
    }
}