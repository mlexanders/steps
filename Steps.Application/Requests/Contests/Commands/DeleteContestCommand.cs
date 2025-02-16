using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Requests.Events.Commands;

public record DeleteContestCommand (Guid ModelId) : IRequest<Result>;

public class DeleteEventCommandHandler : IRequestHandler<DeleteContestCommand, Result>
{
    private readonly IContestManager _contestManager;
    private readonly IMapper _mapper;

    public DeleteEventCommandHandler(IContestManager contestManager, IMapper mapper)
    {
        _contestManager = contestManager;
        _mapper = mapper;
    }

    public async Task<Result> Handle(DeleteContestCommand request, CancellationToken cancellationToken)
    {
        var modelId = request.ModelId;

        await _contestManager.Delete(modelId);
        
        return Result.Ok().SetMessage("Мероприятие удалено!");
    }
}