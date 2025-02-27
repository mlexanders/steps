using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Teams.Commands;

public record CreateTeamCommand(CreateTeamViewModel Model) : IRequest<Result<TeamViewModel>>;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result<TeamViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public CreateTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<TeamViewModel>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request.Model);

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        if (currentUser.Role is not Role.Organizer && !team.OwnerId.Equals(currentUser.Id))
        {
            throw new AppAccessDeniedException();
        }

        var entry = await _unitOfWork.GetRepository<Team>().InsertAsync(team, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        var viewModel = _mapper.Map<TeamViewModel>(entry.Entity);
        
        return Result<TeamViewModel>.Ok(viewModel).SetMessage("Команда создана");
    }
}