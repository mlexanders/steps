using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Requests.Events.Commands;

public record UpdateEventCommand (UpdateEventViewModel Model) : IRequest<Guid>;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
{
    private readonly IEventManager _eventManager;
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IEventManager eventManager, IMapper mapper)
    {
        _eventManager = eventManager;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var _event = _mapper.Map<Event>(model);

        await _eventManager.Update(_event);

        return _event.Id;
    }
}