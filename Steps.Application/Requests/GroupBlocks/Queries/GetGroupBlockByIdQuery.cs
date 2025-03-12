using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Schedules.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.GroupBlocks.Queries;

public record GetGroupBlockByIdQuery(Guid Id) : IRequest<Result<GroupBlockViewModel>>;

public class GetGroupBlockByIdQueryHandler : IRequestHandler<GetGroupBlockByIdQuery, Result<GroupBlockViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGroupBlockByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<GroupBlockViewModel>> Handle(GetGroupBlockByIdQuery request, CancellationToken cancellationToken)
    {
        var view = await _unitOfWork.GetRepository<GroupBlock>().GetFirstOrDefaultAsync(
                        predicate: g => g.Id == request.Id,
                        include: s => s.Include(g => g.Schedule).Include(g => g.FinalSchedule),
                        selector: g => _mapper.Map<GroupBlockViewModel>(g),
                        trackingType: TrackingType.NoTracking)
                    ?? throw new StepsBusinessException("Блок не найден");

        return Result<GroupBlockViewModel>.Ok(view);
    }
}
