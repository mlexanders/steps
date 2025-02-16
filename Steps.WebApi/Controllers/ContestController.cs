using Calabonga.PagedListCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Contests.Commands;
using Steps.Application.Requests.Contests.Queries;
using Steps.Domain.Entities;
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
    
    [HttpGet]
    public async Task<Result<IPagedList<Contest>>> Read([FromQuery] Page page)
    {
        return await _mediator.Send(new GetContestsQuery(page));
    }
    
    [HttpPatch]
    public async Task<Result<Guid>> Update([FromBody] UpdateContestViewModel updateContestViewModel)
    {
        return await _mediator.Send(new UpdateContestCommand(updateContestViewModel));
    }

    [HttpDelete]
    public async Task<Result> Delete(Guid id)
    {
        return await _mediator.Send(new DeleteContestCommand(id));
    }
}