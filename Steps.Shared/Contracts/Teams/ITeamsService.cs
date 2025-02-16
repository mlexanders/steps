using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Shared.Contracts.Teams;

public interface ITeamsService
{
    Task<Result<Guid>> Create(CreateTeamViewModel model);
    Task<Result<TeamViewModel>> GetTeamById(Guid teamId);
}