using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Requests.Events.Commands;

public record CreateEventCommand (CreateEventViewModel Model) : IRequest<Guid>;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventManager _eventManager;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IEventManager eventManager, IMapper mapper)
    {
        _eventManager = eventManager;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var _event = _mapper.Map<Event>(model);

        await _eventManager.Create(_event);

        return _event.Id;
    }
}