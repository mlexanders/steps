using Calabonga.PagedListCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Contests.Commands;
using Steps.Application.Requests.Contests.Queries;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ContestsController : ControllerBase, IContestService
{
    private readonly IMediator _mediator;

    public ContestsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result<Guid>> Create([FromBody] CreateContestViewModel createContestViewModel)
    {
        return await _mediator.Send(new CreateContestCommand(createContestViewModel));
    }

    [HttpGet("{contestId:guid}")]
    public async Task<Result<ContestViewModel>> GetById(Guid contestId)
    {
        return await _mediator.Send(new GetContestByIdQuery(contestId));
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
    public async Task<Result> Delete(Guid id)
    {
        return await _mediator.Send(new DeleteContestCommand(id));
    }
}