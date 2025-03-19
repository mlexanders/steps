using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Helpers;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.PreSchedules.Queries;

public record GetPagedPreScheduledCellsByGroupBlockIdQuery(GetPagedPreScheduledCells Model)
    : SpecificationRequest<PreScheduledCell>(Model.Specification), IRequest<Result<PaggedListViewModel<PreScheduledCellViewModel>>>;

public class GetPagedScheduledCellsByGroupBlockIdHandler : IRequestHandler<GetPagedPreScheduledCellsByGroupBlockIdQuery,
    Result<PaggedListViewModel<PreScheduledCellViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedScheduledCellsByGroupBlockIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaggedListViewModel<PreScheduledCellViewModel>>> Handle(
        GetPagedPreScheduledCellsByGroupBlockIdQuery request, CancellationToken cancellationToken)
    {
        var groupBlockId = request.Model.GroupBlockId;
        var page = request.Model.Page;

        request.AddPredicate(c => c.GroupBlockId.Equals(groupBlockId));
        var scheduledCells = await _unitOfWork.GetRepository<PreScheduledCell>().GetPagedListAsync(
            predicate: request.Predicate,
            include: x =>
                x.Include(s => s.Athlete)
                    .ThenInclude(a => a.Team)
                    .ThenInclude(t => t.Club),
            selector: c => _mapper.Map<PreScheduledCellViewModel>(c),
            orderBy: c => c.OrderBy(cell => cell.SequenceNumber),
            pageIndex: page.PageIndex,
            pageSize: page.PageSize,
            trackingType: TrackingType.NoTracking,
            cancellationToken: cancellationToken);

        return Result<PaggedListViewModel<PreScheduledCellViewModel>>.Ok(scheduledCells.GetView());
    }
}