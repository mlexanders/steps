using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Teams.Commands;

public record DeleteTeamCommand(Guid TeamId) : IRequest<Result>;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;

    public DeleteTeamCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
    }

    public async Task<Result> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var id = request.TeamId;

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        var repository = _unitOfWork.GetRepository<Team>();
        var team = await repository.FindAsync(id) ?? throw new AppNotFoundException("Команда не найдена");

        if (currentUser.Role is not Role.Organizer && !team.OwnerId.Equals(currentUser.Id))
        {
            throw new AppAccessDeniedException();
        }

        repository.Delete(team);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok().SetMessage($"Команда {team.Name} удалена");
    }
}