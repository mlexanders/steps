using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Requests.Events.Commands;

public record UpdateEventCommand (UpdateEventViewModel Model) : IRequest<Result<Guid>>;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Result<Guid>>
{
    private readonly IEventManager _eventManager;
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IEventManager eventManager, IMapper mapper)
    {
        _eventManager = eventManager;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var _event = _mapper.Map<Event>(model);

        await _eventManager.Update(_event);

        return Result<Guid>.Success(_event.Id).SetMessage("Мероприятие успешно обновлено!");
    }
}