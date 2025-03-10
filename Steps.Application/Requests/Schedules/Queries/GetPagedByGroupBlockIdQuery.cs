using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Schedules.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Schedules.Queries;

public record GetPagedScheduledCellsByGroupBlockIdQuery(Guid GroupBlockId, Page Page) : IRequest<Result<PaggedListViewModel<ScheduledCellViewModel>>>;


public class GetPagedScheduledCellsByGroupBlockIdHandler : IRequestHandler<GetPagedScheduledCellsByGroupBlockIdQuery, Result<PaggedListViewModel<ScheduledCellViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetPagedScheduledCellsByGroupBlockIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> Handle(GetPagedScheduledCellsByGroupBlockIdQuery request, CancellationToken cancellationToken)
    {
        var groupBlockId = request.GroupBlockId;
        var page = request.Page;
        
       var scheduledCells = await  _unitOfWork.GetRepository<ScheduledCell>().GetPagedListAsync(
            predicate: c => c.GroupBlockId == groupBlockId,
            selector: c => _mapper.Map<ScheduledCellViewModel>(c),
            orderBy: c => c.OrderBy(cell => cell.SequenceNumber),
            pageIndex: page.PageIndex,
            pageSize: page.PageSize,
            trackingType: TrackingType.NoTracking,
            cancellationToken: cancellationToken);
        
       return Result<PaggedListViewModel<ScheduledCellViewModel>>.Ok(scheduledCells.GetView());
    }
}