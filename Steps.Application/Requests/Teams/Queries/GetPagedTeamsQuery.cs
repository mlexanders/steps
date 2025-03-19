using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Shared.Exceptions;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Teams.Queries;


public record GetPagedTeamsQuery(Page Page, Specification<Team>? Specification)
    : SpecificationRequest<Team>(Specification), IRequest<Result<PaggedListViewModel<TeamViewModel>>>;

public class GetPagedTeamsQueryHandler
    : IRequestHandler<GetPagedTeamsQuery,
        Result<PaggedListViewModel<TeamViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public GetPagedTeamsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<PaggedListViewModel<TeamViewModel>>> Handle(GetPagedTeamsQuery request, CancellationToken cancellationToken)
    {
        var user = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        request.AddPredicate(x => x.OwnerId.Equals(user.Id)); // Только команды пользователя
        
        var views = await _unitOfWork.GetRepository<Team>()
            .GetPagedListAsync(
                selector: (team) => _mapper.Map<TeamViewModel>(team),
                predicate: request.Predicate,
                include: request.Includes,
                pageIndex: request.Page.PageIndex,
                pageSize: request.Page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);
        
        var result = Result<PaggedListViewModel<TeamViewModel>>.Ok(views.GetView());

        return result;
    }
}