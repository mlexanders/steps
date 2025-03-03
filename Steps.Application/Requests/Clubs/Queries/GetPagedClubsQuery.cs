using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Exceptions;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Clubs.Queries;

public record GetPagedClubsQuery(Page Page, Specification<Club>? Specification)
    : SpecificationRequest<Club>(Specification), IRequest<Result<PaggedListViewModel<ClubViewModel>>>;

public class GetPagedClubsQueryHandler
    : IRequestHandler<GetPagedClubsQuery,
        Result<PaggedListViewModel<ClubViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public GetPagedClubsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<PaggedListViewModel<ClubViewModel>>> Handle(GetPagedClubsQuery request,
        CancellationToken cancellationToken)
    {
        var views = await _unitOfWork.GetRepository<Club>()
            .GetPagedListAsync(
                selector: (club) => _mapper.Map<ClubViewModel>(club),
                predicate: request.Predicate,
                include: request.Includes,
                pageIndex: request.Page.PageIndex,
                pageSize: request.Page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);
        
        var result = Result<PaggedListViewModel<ClubViewModel>>.Ok(views.GetView());

        return result;
    }
}