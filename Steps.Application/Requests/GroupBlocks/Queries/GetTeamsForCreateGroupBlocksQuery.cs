using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.GroupBlocks.Queries;


public record GetTeamsForCreateGroupBlocksQuery(Guid ContestId) : IRequest<Result<List<TeamViewModel>>>;

public class GetTeamsForCreateGroupBlocksQueryHandler : IRequestHandler<GetTeamsForCreateGroupBlocksQuery,
    Result<List<TeamViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTeamsForCreateGroupBlocksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<TeamViewModel>>> Handle(GetTeamsForCreateGroupBlocksQuery request, CancellationToken cancellationToken)
    {
        var block = await _unitOfWork.GetRepository<Entry>().GetAllAsync(
                        predicate: e => e.ContestId.Equals(request.ContestId) && e.IsSuccess,
                        selector: g =>   _mapper.Map<TeamViewModel>(g.Team),
                        trackingType: TrackingType.NoTracking)
                    ?? throw new StepsBusinessException("Команды не найдены");

        return Result<List<TeamViewModel>>.Ok(block.ToList());
    }
}