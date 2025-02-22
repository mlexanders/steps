using Calabonga.PagedListCore;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Contests;

public interface IContestService
{
    Task<Result<Guid>> Create(CreateContestViewModel createContestViewModel);
    Task<Result<Guid>> Update(UpdateContestViewModel updateContestViewModel);
    Task<Result<ContestViewModel>> GetById(Guid clubId);
    Task<Result<PaggedListViewModel<ContestViewModel>>> GetPaged(Page page);
    Task<Result> Delete(Guid id);
}