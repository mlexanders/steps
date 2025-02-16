using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Shared;

namespace Steps.Application.Requests.Contests.Commands;

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