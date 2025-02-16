using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Requests.Contests.Commands;

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
        var contest = _mapper.Map<Contest>(model);

        await _contestManager.Create(contest);

        return Result<Guid>.Ok(contest.Id).SetMessage("Мероприятие успешно создано!");
    }
}