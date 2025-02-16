using Calabonga.PagedListCore;
using Steps.Domain.Entities;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Interfaces;

public interface IContestManager
{
    Task Create(Contest contest);
    Task<IPagedList<ContestViewModel>> Read(Page page);
    Task Update(Contest model);
    Task Delete(Guid id);
}