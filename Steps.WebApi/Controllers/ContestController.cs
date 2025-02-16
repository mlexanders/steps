using Calabonga.PagedListCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Contests.Queries;
using Steps.Application.Requests.Events.Commands;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Events;
using Steps.Shared.Contracts.Events.ViewModels;

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
    
    [HttpPost("[Action]")]
    public async Task<Result<Guid>> Create([FromBody] CreateContestViewModel createContestViewModel)
    {
        return await _mediator.Send(new CreateContestCommand(createContestViewModel));
    }
    
    [HttpGet("[Action]")]
    public async Task<Result<IPagedList<Contest>>?> Read(int take, int skip)
    {
        return await _mediator.Send(new GetContestsQuery(take, skip));
    }
    
    [HttpPut("[Action]")]
    public async Task<Result<Guid>> Update(UpdateContestViewModel updateContestViewModel)
    {
        return await _mediator.Send(new UpdateContestCommand(updateContestViewModel));
    }

    [HttpDelete("[Action]")]
    public async Task<Result> Delete(Guid id)
    {
        return await _mediator.Send(new DeleteContestCommand(id));
    }
}