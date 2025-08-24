using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.TestResults.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.SoloResults.Queries;

public record GetPaggedSoloResultQuery(Page Page, Specification<SoloResult>? Specification)
    : SpecificationRequest<SoloResult>(Specification), IRequest<Result<PaggedListViewModel<SoloResultViewModel>>>;

public class GetPagedSoloResultsQueryHandler
    : IRequestHandler<GetPaggedSoloResultQuery,
        Result<PaggedListViewModel<SoloResultViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedSoloResultsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaggedListViewModel<SoloResultViewModel>>> Handle(GetPaggedSoloResultQuery request,
        CancellationToken cancellationToken)
    {
        var views = await _unitOfWork.GetRepository<SoloResult>()
            .GetPagedListAsync(
                selector: soloResult => _mapper.Map<SoloResultViewModel>(soloResult),
                predicate: request.Predicate,
                include: request.Includes,
                pageIndex: request.Page.PageIndex,
                pageSize: request.Page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);

        var result = Result<PaggedListViewModel<SoloResultViewModel>>.Ok(views.GetView());

        return result;
    }
}
