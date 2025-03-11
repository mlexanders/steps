using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.TeamsFeature.Services;

public class TeamsManager : EntityManagerBase<Team, TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>
{
    public TeamsManager(ITeamsService contestsService) : base(contestsService)
    {
    }
}