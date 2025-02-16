using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Definitions;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Teams.Commands;

public record CreateTeamCommand(CreateTeamViewModel Model) : IRequest<Result<Guid>>;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private ISecurityService _securityService;

    public CreateTeamCommandHandler(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper,
        ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<Guid>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request.Model);

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        if (currentUser.Role is not Role.Organizer && !team.OwnerId.Equals(currentUser.Id))
        {
            throw new AppAccessDeniedException();
        }

        var entity = _unitOfWork.GetRepository<Team>().Insert(team);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Ok(entity.Id).SetMessage("Команда создана");
    }
}