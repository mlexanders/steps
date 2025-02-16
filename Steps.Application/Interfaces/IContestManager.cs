using Calabonga.PagedListCore;
using Steps.Domain.Entities;

namespace Steps.Application.Interfaces;

public interface IContestManager
{
    Task Create(Contest contest);
    Task<IPagedList<Contest>> Read(int take, int skip);
    Task Update(Contest model);
    Task Delete(Guid id);
}