using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Infrastructure.Data;

namespace Steps.Application.Requests.Teams.Queries;

public record GetTeamByIdQuery(Guid TeamId) : IRequest<TeamViewModel?>;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamViewModel?>
{
    private readonly ApplicationDbContext _dbContext;

    public GetTeamByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TeamViewModel?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _dbContext.Teams
            .FirstOrDefaultAsync(t => t.Id.Equals(request.TeamId), cancellationToken);

        return team == null
            ? null
            : new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Owner = team.Owner
            };
    }
}