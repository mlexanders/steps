using Calabonga.PagedListCore;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Requests.Contests.Queries;

public record GetContestsQuery (Page Page) : IRequest<Result<IPagedList<ContestViewModel>>>;

public class GetEventsQueryHandler : IRequestHandler<GetContestsQuery, Result<IPagedList<ContestViewModel>>>
{
    private readonly IContestManager _contestManager;

    public GetEventsQueryHandler(IContestManager contestManager)
    {
        _contestManager = contestManager;
    }

    public async Task<Result<IPagedList<ContestViewModel>>> Handle(GetContestsQuery request, CancellationToken cancellationToken)
    {
        var contests = await _contestManager.Read(request.Page);
        return Result<IPagedList<ContestViewModel>>.Ok(contests);
    }
}