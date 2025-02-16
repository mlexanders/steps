using Calabonga.PagedListCore;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Contests;

public interface IContestService
{
    Task<Result<Guid>> Create(CreateContestViewModel createContestViewModel);
    Task<Result<IPagedList<Contest>>> Read(Page page);
    Task<Result<Guid>> Update(UpdateContestViewModel updateContestViewModel);
    Task<Result> Delete(Guid id);
}