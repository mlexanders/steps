using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Shared.Contracts.Teams;

public interface ITeamsService
{
    Task<Result<Guid>> Create(CreateTeamViewModel command);
    Task<Result<TeamViewModel>> GetTeamById(Guid teamId);
}