using Calabonga.PagedListCore;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Shared.Contracts.Events;

public interface IContestService
{
    Task<Result<Guid>> Create(CreateContestViewModel createContestViewModel);
    Task<Result<IPagedList<Contest>>?> Read(int take, int skip);
    Task<Result<Guid>> Update(UpdateContestViewModel updateContestViewModel);
    Task<Result> Delete(Guid id);
}