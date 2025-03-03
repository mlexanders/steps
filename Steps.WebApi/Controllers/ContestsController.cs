using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Contests.Commands;
using Steps.Application.Requests.Contests.Queries;
using Steps.Filters.Filters;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class ContestsController : ControllerBase, IContestsService
{
    private readonly IMediator _mediator;

    public ContestsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result<ContestViewModel>> Create([FromBody] CreateContestViewModel createContestViewModel)
    {
        return await _mediator.Send(new CreateContestCommand(createContestViewModel));
    }

    [HttpGet("{contestId:guid}")]
    public async Task<Result<ContestViewModel>> GetById(Guid contestId)
    {
        return await _mediator.Send(new GetContestByIdQuery(contestId));
    }

    public Task<Result<List<ContestViewModel>>> GetBy(FilterGroup filter)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<Result<PaggedListViewModel<ContestViewModel>>> GetPaged([FromQuery] Page page)
    {
        return (await _mediator.Send(new GetContestsQuery(page)));
    }
    
    [HttpPatch]
    public async Task<Result<Guid>> Update([FromBody] UpdateContestViewModel updateContestViewModel)
    {
        return await _mediator.Send(new UpdateContestCommand(updateContestViewModel));
    }

    [HttpDelete("{contestId:guid}")]
    public async Task<Result> Delete(Guid contestId)
    {
        return await _mediator.Send(new DeleteContestCommand(contestId));
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