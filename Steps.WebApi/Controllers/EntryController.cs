using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Contests.Commands;
using Steps.Application.Requests.Entries.Commands;
using Steps.Shared;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class EntryController : ControllerBase, IEntryService
{
    private readonly IMediator _mediator;

    public EntryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result<Guid>> Create([FromBody] CreateEntryViewModel entryViewModel)
    {
        return await _mediator.Send(new CreateEntryCommand(entryViewModel));
    }
    
    [HttpPost("Accept-entry")]
    public async Task<Result> AcceptEntry(Guid entryId)
    {
        return await _mediator.Send(new AcceptEntryCommand(entryId));
    }
}