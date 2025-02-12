using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Teams.Commands;
using Steps.Application.Requests.Teams.Queries;
using Steps.Shared;
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
    public async Task<Result<Guid>> Create([FromBody] CreateTeamViewModel command)
    {
        var teamId = await _mediator.Send(new CreateTeamCommand(command));
        return Result<Guid>.Success(teamId).SetTraceId(HttpContext.TraceIdentifier);
    }

    // [HttpPut("{teamId:guid}")]
    // public async Task<IActionResult> UpdateTeam(Guid teamId, [FromBody] UpdateTeamCommand command)
    // {
    //     if (teamId != command.TeamId) return BadRequest("Invalid team ID");
    //
    //     var result = await _mediator.Send(command);
    //     return result ? NoContent() : NotFound();
    // }

    // [HttpDelete("{teamId:guid}")]
    // public async Task<IActionResult> DeleteTeam(Guid teamId)
    // {
    //     var result = await _mediator.Send(new DeleteTeamCommand(teamId));
    //     return result ? NoContent() : NotFound();
    // }

    [HttpGet("{teamId:guid}")]
    public async Task<Result<TeamViewModel>> GetTeamById(Guid teamId)
    {
        var team = await _mediator.Send(new GetTeamByIdQuery(teamId));
        
        return team is null
            ? Result<TeamViewModel>.NotFound().SetTraceId(HttpContext.TraceIdentifier)
            : Result<TeamViewModel>.Success(team).SetTraceId(HttpContext.TraceIdentifier);
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAllTeams()
    // {
    //     var teams = await _mediator.Send(new GetAllTeamsQuery());
    //     return Ok(teams);
    // }
}