using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Requests.Events.Commands;

public record CreateContestCommand (CreateContestViewModel Model) : IRequest<Result<Guid>>;

public class CreateEventCommandHandler : IRequestHandler<CreateContestCommand, Result<Guid>>
{
    private readonly IContestManager _contestManager;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IContestManager contestManager, IMapper mapper)
    {
        _contestManager = contestManager;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateContestCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var _event = _mapper.Map<Contest>(model);

        await _contestManager.Create(_event);

        return Result<Guid>.Success(_event.Id).SetMessage("Мероприятие успешно создано!");
    }
}