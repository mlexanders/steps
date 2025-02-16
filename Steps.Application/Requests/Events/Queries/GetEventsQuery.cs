using AutoMapper;
using Calabonga.PagedListCore;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Requests.Events.Commands;
using Steps.Domain.Entities;
using Steps.Shared;

namespace Steps.Application.Requests.Events.Queries;

public record GetEventsQuery (int take, int skip) : IRequest<Result<IPagedList<Event>>?>;

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, Result<IPagedList<Event>>?>
{
    private readonly IEventManager _eventManager;
    private readonly IMapper _mapper;

    public GetEventsQueryHandler(IEventManager eventManager, IMapper mapper)
    {
        _eventManager = eventManager;
        _mapper = mapper;
    }

    public async Task<Result<IPagedList<Event>>?> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventManager.Read(request.take, request.skip);
        return Result<IPagedList<Event>?>.Success(events);
    }
}