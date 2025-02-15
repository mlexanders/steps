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
        var eventId = await _mediator.Send(new CreateEventCommand(createEventViewModel));
        return Result<Guid>.Success(eventId).SetTraceId(HttpContext.TraceIdentifier);
    }
    
    [HttpGet("[Action]")]
    public async Task<Result<List<Event>>> Read(int take, int skip)
    {
        var events = await _mediator.Send(new GetEventsQuery(take, skip));
        return Result<List<Event>>.Success(events).SetTraceId(HttpContext.TraceIdentifier);
    }
    
    [HttpPut("[Action]")]
    public async Task<Result<Guid>> Update(UpdateEventViewModel updateEventViewModel)
    {
        var eventId = await _mediator.Send(new UpdateEventCommand(updateEventViewModel));
        return Result<Guid>.Success(eventId).SetTraceId(HttpContext.TraceIdentifier);
    }

    [HttpDelete("[Action]")]
    public async Task<Result> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEventCommand(id));
        return Result.Success("Entity deleted");
    }
}