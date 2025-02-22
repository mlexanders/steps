using AutoMapper;
using Calabonga.PagedListCore;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Requests.Contests.Queries;

public record GetContestsQuery (Page Page) : IRequest<Result<IPagedList<ContestViewModel>>>;

public class GetEventsQueryHandler : IRequestHandler<GetContestsQuery, Result<IPagedList<ContestViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IPagedList<ContestViewModel>>> Handle(GetContestsQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page;
        
        var repository = _unitOfWork.GetRepository<Contest>();

        var contests = await repository.GetPagedListAsync(
            selector: contest => _mapper.Map<ContestViewModel>(contest),
            pageIndex: page.PageIndex,
            pageSize: page.PageSize,
            cancellationToken: cancellationToken,
            trackingType: TrackingType.NoTracking);

        return Result<IPagedList<ContestViewModel>>.Ok(contests);
    }
}