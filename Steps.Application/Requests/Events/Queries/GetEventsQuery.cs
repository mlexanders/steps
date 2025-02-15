using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Requests.Events.Commands;
using Steps.Domain.Entities;

namespace Steps.Application.Requests.Events.Queries;

public record GetEventsQuery (int take, int skip) : IRequest<List<Event>?>;

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<Event>?>
{
    private readonly IEventManager _eventManager;
    private readonly IMapper _mapper;

    public GetEventsQueryHandler(IEventManager eventManager, IMapper mapper)
    {
        _eventManager = eventManager;
        _mapper = mapper;
    }

    public async Task<List<Event>?> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        return await _eventManager.Read(request.take, request.skip);
    }
}