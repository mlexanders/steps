using AutoMapper;
using Calabonga.PagedListCore;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Clubs.Queries;

public record GetPagedClubsQuery(Page Page) : IRequest<Result<IPagedList<ClubViewModel>>>;

public class CategoryGetPagedRequestHandler
    : IRequestHandler<GetPagedClubsQuery,
        Result<IPagedList<ClubViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public CategoryGetPagedRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<IPagedList<ClubViewModel>>> Handle(GetPagedClubsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();
        
        var views = await _unitOfWork.GetRepository<Club>()
            .GetPagedListAsync(
                selector: (club) => _mapper.Map<ClubViewModel>(club),
                predicate: c => c.OwnerId.Equals(user.Id), // TODO: сча получение только своих клубов
                pageIndex: request.Page.PageIndex,
                pageSize: request.Page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);
        
        var result = Result<IPagedList<ClubViewModel>>.Ok(views);

        return result;
    }
}