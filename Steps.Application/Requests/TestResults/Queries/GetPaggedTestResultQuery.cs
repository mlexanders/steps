using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.TestResults.ViewModels;
using Steps.Shared.Exceptions;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.TestResults.Queries;

public record GetPaggedTestResultQuery(Page Page, Specification<TestResult>? Specification)
    : SpecificationRequest<TestResult>(Specification), IRequest<Result<PaggedListViewModel<TestResultViewModel>>>;

public class GetPagedTeamsQueryHandler
    : IRequestHandler<GetPaggedTestResultQuery,
        Result<PaggedListViewModel<TestResultViewModel>>>
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

    public async Task<Result<PaggedListViewModel<TestResultViewModel>>> Handle(GetPaggedTestResultQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        var views = await _unitOfWork.GetRepository<TestResult>()
            .GetPagedListAsync(
                (team) => _mapper.Map<TestResultViewModel>(team),
                request.Predicate,
                include: request.Includes,
                pageIndex: request.Page.PageIndex,
                pageSize: request.Page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);

        var result = Result<PaggedListViewModel<TestResultViewModel>>.Ok(views.GetView());

        return result;
    }
}