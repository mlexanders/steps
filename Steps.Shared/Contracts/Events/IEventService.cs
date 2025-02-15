using Steps.Domain.Entities;
using Steps.Shared.Contracts.Events.ViewModels;

namespace Steps.Shared.Contracts.Events;

public interface IEventService
{
    Task<Result<Guid>> Create(CreateEventViewModel createEventViewModel);
    Task<Result<List<Event>>> Read(int take, int skip);
    Task<Result<Guid>> Update(UpdateEventViewModel updateEventViewModel);
    Task<Result> Delete(Guid id);
}