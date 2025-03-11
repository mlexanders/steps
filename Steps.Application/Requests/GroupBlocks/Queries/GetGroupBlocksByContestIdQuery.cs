using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.GroupBlocks.Queries;

public record GetGroupBlocksByContestIdQuery(Guid ContestId) : IRequest<Result<List<GroupBlockViewModel>>>;

public class GetGroupBlocksByContestIdQueryHandler : IRequestHandler<GetGroupBlocksByContestIdQuery,
    Result<List<GroupBlockViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGroupBlocksByContestIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GroupBlockViewModel>>> Handle(GetGroupBlocksByContestIdQuery request, CancellationToken cancellationToken)
    {
        var block = await _unitOfWork.GetRepository<GroupBlock>().GetAllAsync(
                        predicate: s => s.ContestId.Equals(request.ContestId),
                        selector: g => _mapper.Map<GroupBlockViewModel>(g),
                        trackingType: TrackingType.NoTracking)
                    ?? throw new StepsBusinessException("Блоки не найдены");

        return Result<List<GroupBlockViewModel>>.Ok(block.ToList());
    }
}