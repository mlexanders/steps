using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Contests.Queries;

public record GetContestsQuery(Page Page, Specification<Contest>? Specification)
    : SpecificationRequest<Contest>(Specification), IRequest<Result<PaggedListViewModel<ContestViewModel>>>;

public class GetContestsQueryHandler : IRequestHandler<GetContestsQuery, Result<PaggedListViewModel<ContestViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetContestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaggedListViewModel<ContestViewModel>>> Handle(GetContestsQuery request,
        CancellationToken cancellationToken)
    {
        var page = request.Page;

        var repository = _unitOfWork.GetRepository<Contest>();

        var contests = await repository.GetPagedListAsync(
            selector: contest => _mapper.Map<ContestViewModel>(contest),
            orderBy: contest => contest.OrderBy(c => c.StartDate),
            predicate: request.Predicate,
            include: request.Includes,
            pageIndex: page.PageIndex,
            pageSize: page.PageSize,
            cancellationToken: cancellationToken,
            trackingType: TrackingType.NoTracking);

        return Result<PaggedListViewModel<ContestViewModel>>.Ok(contests.GetView());
    }
}