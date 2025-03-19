using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Contests.Commands;
using Steps.Application.Requests.Contests.Queries;
using Steps.Domain.Entities;
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

    [HttpPost("[action]")]
    public async Task<Result<PaggedListViewModel<ContestViewModel>>> GetPaged([FromQuery] Page page,
        [FromBody] Specification<Contest>? specification)
    {
        return (await _mediator.Send(new GetContestsQuery(page, specification)));
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

    [HttpGet("[action]")]
    public Task<Result<List<ContestViewModel>>> GetByTimeInterval([FromQuery] GetContestByInterval criteria)
    {
        return _mediator.Send(new GetByTimeIntervalQuery(criteria));
    }

    [HttpPost("[action]/{contestId:guid}")]
    public Task<Result> CloseCollectingEntries(Guid contestId)
    {
        return _mediator.Send(new CloseCollectingContestCommand(contestId));
    }
}


