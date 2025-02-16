using Calabonga.PagedListCore;
using Steps.Domain.Entities;
using Steps.Shared.Contracts;

namespace Steps.Application.Interfaces;

public interface IContestManager
{
    Task Create(Contest contest);
    Task<IPagedList<Contest>> Read(Page page);
    Task Update(Contest model);
    Task Delete(Guid id);
}