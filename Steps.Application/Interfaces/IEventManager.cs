using Steps.Domain.Entities;

namespace Steps.Application.Interfaces;

public interface IEventManager
{
    Task Create(Event model);
    Task<List<Event>?> Read(int take, int skip);
    Task Update(Event model);
    Task Delete(Guid id);
}