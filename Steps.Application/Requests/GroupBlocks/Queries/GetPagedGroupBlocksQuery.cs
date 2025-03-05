using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Helpers;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Schedules.ViewModels;
using Steps.Shared.Exceptions;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.GroupBlocks.Queries;

public record GetPagedGroupBlocksQuery(Page Page, Specification<GroupBlock>? Specification) :
    SpecificationRequest<GroupBlock>(Specification),
    IRequest<Result<PaggedListViewModel<GroupBlockViewModel>>>;

public class GetPagedGroupBlocksQueryHandler : IRequestHandler<GetPagedGroupBlocksQuery,
    Result<PaggedListViewModel<GroupBlockViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedGroupBlocksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaggedListViewModel<GroupBlockViewModel>>> Handle(GetPagedGroupBlocksQuery request,
        CancellationToken cancellationToken)
    {
        var block = await _unitOfWork.GetRepository<GroupBlock>().GetPagedListAsync(
                        include: s => s.Include(g => g.GroupBlockCells),
                        selector: g => _mapper.Map<GroupBlockViewModel>(g),
                        pageIndex: request.Page.PageIndex,
                        pageSize: request.Page.PageSize,
                        trackingType: TrackingType.NoTracking,
                        cancellationToken: cancellationToken)
                    ?? throw new StepsBusinessException("Блоки не найдены");

        return Result<PaggedListViewModel<GroupBlockViewModel>>.Ok(block.GetView());
    }
}