using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Requests.Events.Commands;

public record DeleteEventCommand (Guid ModelId) : IRequest;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventManager _eventManager;
    private readonly IMapper _mapper;

    public DeleteEventCommandHandler(IEventManager eventManager, IMapper mapper)
    {
        _eventManager = eventManager;
        _mapper = mapper;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var modelId = request.ModelId;

        await _eventManager.Delete(modelId);
    }
}