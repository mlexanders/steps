using AutoMapper;
using Calabonga.PagedListCore;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Contests.Queries;


public record GetContestByIdQuery (Guid ContestId) : IRequest<Result<ContestViewModel>>;

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
                trackingType: TrackingType.NoTracking);

        if (contest == null) throw new AppNotFoundException("Мероприятие не найдено");
        
        return Result<ContestViewModel>.Ok(contest);
    }
}