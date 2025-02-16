using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Shared;

namespace Steps.Application.Requests.Teams.Queries;

public record GetTeamByIdQuery(Guid TeamId) : IRequest<Result<TeamViewModel>>;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Result<TeamViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTeamByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<TeamViewModel>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.GetRepository<Team>()
            .GetFirstOrDefaultAsync(
                predicate: t => t.Id.Equals(request.TeamId),
                trackingType: TrackingType.NoTracking);

        if (team == null) return Result<TeamViewModel>.Fail("Команда не найдена");

        var viewModel = _mapper.Map<TeamViewModel>(team);

        return Result<TeamViewModel>.Ok(viewModel);
    }
}