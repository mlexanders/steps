using Calabonga.PagedListCore;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Shared.Contracts.Clubs;

public interface IClubsService
{
    Task<Result<ClubViewModel>> Create(CreateClubViewModel model);
    Task<Result> Update(UpdateClubViewModel model);
    Task<Result<ClubViewModel>> GetClubById(Guid clubId);
    Task<Result<IPagedList<ClubViewModel>>> GetPaged(Page page);
    Task<Result> Delete(Guid clubId);
}