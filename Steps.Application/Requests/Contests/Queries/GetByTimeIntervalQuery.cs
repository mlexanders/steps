using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Requests.Contests.Queries;

public record GetByTimeIntervalQuery(GetContestByInterval Criteria) : IRequest<Result<List<ContestViewModel>>>;

public class GetByTimeIntervalQueryHandler : IRequestHandler<GetByTimeIntervalQuery,Result<List<ContestViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByTimeIntervalQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<List<ContestViewModel>>> Handle(GetByTimeIntervalQuery request, CancellationToken cancellationToken)
    {
        var criteria = request.Criteria;
        
        var contests = await _unitOfWork.GetRepository<Contest>()
            .GetAllAsync(
                predicate: c => c.StartDate > criteria.Start && c.EndDate < criteria.End,
                include: c=> c.Include(c => c.PreScheduleFile).Include(c => c.FinalScheduleFile),
                selector: c => _mapper.Map<ContestViewModel>(c),
                trackingType: TrackingType.NoTracking);
        
        return Result<List<ContestViewModel>>.Ok(contests.ToList());
    }
}
