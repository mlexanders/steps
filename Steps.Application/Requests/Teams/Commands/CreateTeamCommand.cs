using Calabonga.UnitOfWork;
using MediatR;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;

namespace Steps.Application.Requests.Teams.Commands;

public record CreateTeamCommand(CreateTeamViewModel Team) : IRequest<Guid>;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeamCommandHandler(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team
        {
            Name = request.Team.Name,
            OwnerId = request.Team.OwnerId
        };

        var entity = _unitOfWork.GetRepository<Team>().Insert(team);
        await _unitOfWork.SaveChangesAsync();

        return entity.Id;
    }
}