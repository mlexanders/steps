using Steps.Client.Features.Organizer.Services;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.Organizer.TeamsFeature.Services;

public class TeamsManager : BaseEntityManager<Team, TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>
{
    public TeamsManager(ITeamsService contestsService) : base(contestsService)
    {
    }
}