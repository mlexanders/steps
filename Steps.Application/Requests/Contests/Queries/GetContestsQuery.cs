using AutoMapper;
using Calabonga.PagedListCore;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Requests.Events.Commands;
using Steps.Domain.Entities;
using Steps.Shared;

namespace Steps.Application.Requests.Events.Queries;

public record GetContestsQuery (int take, int skip) : IRequest<Result<IPagedList<Contest>>?>;

public class GetEventsQueryHandler : IRequestHandler<GetContestsQuery, Result<IPagedList<Contest>>?>
{
    private readonly IContestManager _contestManager;
    private readonly IMapper _mapper;

    public GetEventsQueryHandler(IContestManager contestManager, IMapper mapper)
    {
        _contestManager = contestManager;
        _mapper = mapper;
    }

    public async Task<Result<IPagedList<Contest>>?> Handle(GetContestsQuery request, CancellationToken cancellationToken)
    {
        var events = await _contestManager.Read(request.take, request.skip);
        return Result<IPagedList<Contest>?>.Success(events);
    }
}