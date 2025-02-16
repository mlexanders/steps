using AutoMapper;
using Calabonga.PagedListCore;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;

namespace Steps.Application.Requests.Contests.Queries;

public record GetContestsQuery (int Take, int Skip) : IRequest<Result<IPagedList<Contest>>>;

public class GetEventsQueryHandler : IRequestHandler<GetContestsQuery, Result<IPagedList<Contest>>?>
{
    private readonly IContestManager _contestManager;

    public GetEventsQueryHandler(IContestManager contestManager)
    {
        _contestManager = contestManager;
    }

    public async Task<Result<IPagedList<Contest>>?> Handle(GetContestsQuery request, CancellationToken cancellationToken)
    {
        var contests = await _contestManager.Read(request.Take, request.Skip);
        return Result<IPagedList<Contest>>.Ok(contests);
    }
}