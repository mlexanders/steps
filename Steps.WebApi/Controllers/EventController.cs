using Calabonga.PagedListCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Events.Commands;
using Steps.Application.Requests.Events.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Events;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class EventController : ControllerBase, IEventService
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("[Action]")]
    public async Task<Result<Guid>> Create([FromBody] CreateEventViewModel createEventViewModel)
    {
        return await _mediator.Send(new CreateEventCommand(createEventViewModel));
    }
    
    [HttpGet("[Action]")]
    public async Task<Result<IPagedList<Event>>?> Read(int take, int skip)
    {
        return await _mediator.Send(new GetEventsQuery(take, skip));
    }
    
    [HttpPut("[Action]")]
    public async Task<Result<Guid>> Update(UpdateEventViewModel updateEventViewModel)
    {
        return await _mediator.Send(new UpdateEventCommand(updateEventViewModel));
    }

    [HttpDelete("[Action]")]
    public async Task<Result> Delete(Guid id)
    {
        return await _mediator.Send(new DeleteEventCommand(id));
    }
}