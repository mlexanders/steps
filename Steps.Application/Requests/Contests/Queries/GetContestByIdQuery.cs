using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Contests.Queries;

public record GetContestByIdQuery(Guid ContestId) : IRequest<Result<ContestViewModel>>;

public class GetContestByIdQueryHandler : IRequestHandler<GetContestByIdQuery, Result<ContestViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<ContestViewModel>> Handle(GetContestByIdQuery request, CancellationToken cancellationToken)
    {
        var contest = await _unitOfWork.GetRepository<Contest>()
            .GetFirstOrDefaultAsync(
                predicate: c => c.Id.Equals(request.ContestId),
                selector: c => _mapper.Map<ContestViewModel>(c),
                include: x => x.Include(a => a.Judges).Include(a => a.Counters),
                orderBy: c => c.OrderByDescending(o => o.StartDate),
                trackingType: TrackingType.NoTracking);

        if (contest == null) throw new AppNotFoundException("Мероприятие не найдено");

        return Result<ContestViewModel>.Ok(contest);
    }
}
