using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Shared.Contracts.Teams;

public interface ITeamsService : ICrudService<TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>;