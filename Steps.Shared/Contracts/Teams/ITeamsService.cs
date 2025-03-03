using System.Linq.Expressions;
using System.Web;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Shared.Contracts.Teams;

public interface ITeamsService : ICrudService<Team, TeamViewModel, CreateTeamViewModel, UpdateTeamViewModel>;
