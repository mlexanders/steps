using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Helpers;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Schedules.Queries;

public record GetPagedScheduledCellsByGroupBlockIdQuery(GetPagedScheduledCellsViewModel Model)
    : SpecificationRequest<ScheduledCell>(Model.Specification), IRequest<Result<PaggedListViewModel<ScheduledCellViewModel>>>;

public class GetPagedScheduledCellsByGroupBlockIdHandler : IRequestHandler<GetPagedScheduledCellsByGroupBlockIdQuery,
    Result<PaggedListViewModel<ScheduledCellViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedScheduledCellsByGroupBlockIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PaggedListViewModel<ScheduledCellViewModel>>> Handle(
        GetPagedScheduledCellsByGroupBlockIdQuery request, CancellationToken cancellationToken)
    {
        var groupBlockId = request.Model.GroupBlockId;
        var page = request.Model.Page;

        request.AddPredicate(c => c.GroupBlockId.Equals(groupBlockId));
        var scheduledCells = await _unitOfWork.GetRepository<ScheduledCell>().GetPagedListAsync(
            predicate: request.Predicate,
            include: x =>
                x.Include(s => s.Athlete)
                    .ThenInclude(a => a.Team)
                    .ThenInclude(t => t.Club),
            selector: c => _mapper.Map<ScheduledCellViewModel>(c),
            orderBy: c => c.OrderBy(cell => cell.SequenceNumber),
            pageIndex: page.PageIndex,
            pageSize: page.PageSize,
            trackingType: TrackingType.NoTracking,
            cancellationToken: cancellationToken);

        return Result<PaggedListViewModel<ScheduledCellViewModel>>.Ok(scheduledCells.GetView());
    }
}