using Calabonga.PagedListCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Contests.Commands;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ContestController : ControllerBase, IContestService
{
    private readonly IMediator _mediator;

    public ContestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result<Guid>> Create([FromBody] CreateContestViewModel createContestViewModel)
    {
        return await _mediator.Send(new CreateContestCommand(createContestViewModel));
    }

    [HttpGet("{contestId:guid}")]
    public Task<Result<ContestViewModel>> GetById(Guid contestId)
    {
        throw new NotImplementedException(); //TODO:
    }

    [HttpGet]
    public async Task<Result<IPagedList<ContestViewModel>>> GetPaged([FromQuery] Page page)
    {
        throw new NotImplementedException(); //TODO:

        // return await _mediator.Send(new GetContestsQuery(page));
    }
    
    [HttpPatch]
    public async Task<Result<Guid>> Update([FromBody] UpdateContestViewModel updateContestViewModel)
    {
        return await _mediator.Send(new UpdateContestCommand(updateContestViewModel));
    }

    [HttpDelete("{contestId:guid}")]
    public async Task<Result> Delete(Guid id)
    {
        return await _mediator.Send(new DeleteContestCommand(id));
    }
    
    [HttpPost("Generate-group-blocks")]
    public async Task<Result> GenerateGroupBlocks(Guid contestId, int athletesCount)
    {
        return await _mediator.Send(new GenerateGroupBlocksCommand(contestId, athletesCount));
    }
    
    [HttpPost("Check-athlete-appeared")]
    public async Task<Result> CheckAthlete(Guid athleteId, Guid contestId, bool isAppeared)
    {
        return await _mediator.Send(new CheckAthleteCommand(athleteId, contestId, isAppeared));
    }

    [HttpPost("Close-collecting")]
    public async Task<Result> CloseCollectingEntries(Guid contestId)
    {
        return await _mediator.Send(new CloseCollectingContestCommand(contestId));
    }
}