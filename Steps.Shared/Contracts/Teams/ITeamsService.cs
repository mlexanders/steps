using Calabonga.PagedListCore;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Shared.Contracts.Teams;

public interface ITeamsService
{
    Task<Result<Guid>> Create(CreateTeamViewModel model);
    Task<Result> Update(UpdateTeamViewModel model);

    Task<Result<TeamViewModel>> GetById(Guid teamId);
    Task<Result<IPagedList<TeamViewModel>>> GetPaged(Page page);
    Task<Result> Delete(Guid teamId);

}