using MediatR;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Application.Requests.Teams.Queries;

public record GetTeamByIdQuery(Guid TeamId) : IRequest<TeamViewModel?>;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamViewModel?>
{

    public GetTeamByIdQueryHandler()
    {
    }

    public async Task<TeamViewModel?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        // var team = await _dbContext.Teams
        //     .FirstOrDefaultAsync(t => t.Id.Equals(request.TeamId), cancellationToken);
        //
        // return team == null
        //     ? null
        //     : new TeamViewModel
        //     {
        //         Id = team.Id,
        //         Name = team.Name,
        //         Owner = team.Owner
        //     };
        throw new NotImplementedException();
    }
}