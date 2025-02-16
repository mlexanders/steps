using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Application.Requests.Events.Commands;

public record UpdateContestCommand (UpdateContestViewModel Model) : IRequest<Result<Guid>>;

public class UpdateEventCommandHandler : IRequestHandler<UpdateContestCommand, Result<Guid>>
{
    private readonly IContestManager _contestManager;
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IContestManager contestManager, IMapper mapper)
    {
        _contestManager = contestManager;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateContestCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var contest = _mapper.Map<Contest>(model);

        await _contestManager.Update(contest);

        return Result<Guid>.Success(contest.Id).SetMessage("Мероприятие успешно обновлено!");
    }
}