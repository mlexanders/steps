using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Helpers;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Schedules.Queries;

public record GetPagedFinalScheduledCellsByGroupBlockIdQuery(GetPagedFinalScheduledCells Model)
    : SpecificationRequest<FinalScheduledCell>(Model.Specification),
        IRequest<Result<PaggedListViewModel<FinalScheduledCellViewModel>>>;

public class GetPagedFinalScheduledCellsByGroupBlockIdQueryHandler 
    : IRequestHandler<GetPagedFinalScheduledCellsByGroupBlockIdQuery,
    Result<PaggedListViewModel<FinalScheduledCellViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedFinalScheduledCellsByGroupBlockIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaggedListViewModel<FinalScheduledCellViewModel>>> Handle(GetPagedFinalScheduledCellsByGroupBlockIdQuery request, CancellationToken cancellationToken)
    {
        var groupBlockId = request.Model.GroupBlockId;
        var page = request.Model.Page;

        request.AddPredicate(cell => cell.GroupBlockId.Equals(groupBlockId));
        
        var scheduledCells = await _unitOfWork.GetRepository<FinalScheduledCell>().GetPagedListAsync(
            predicate: request.Predicate,
            include: x =>
                x.Include(s => s.Athlete)
                    ///TODO: денормализовать таблицу, убрать два джойна Team & Club
                    .ThenInclude(a => a.Team)
                    .ThenInclude(t => t.Club),
            selector: c => _mapper.Map<FinalScheduledCellViewModel>(c),
            orderBy: c => c.OrderBy(cell => cell.SequenceNumber),
            pageIndex: page.PageIndex,
            pageSize: page.PageSize,
            trackingType: TrackingType.NoTracking,
            cancellationToken: cancellationToken);

        return Result<PaggedListViewModel<FinalScheduledCellViewModel>>.Ok(scheduledCells.GetView());
    }
}

