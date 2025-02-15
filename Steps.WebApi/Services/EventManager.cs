using Calabonga.UnitOfWork;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;

namespace Steps.Services.WebApi.Services;

public class EventManager (IUnitOfWork<ApplicationDbContext> unitOfWork) : IEventManager
{
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork = unitOfWork;
    
    public async Task Create(Event model)
    {
        var repository = _unitOfWork.GetRepository<Event>();

        await repository.InsertAsync(model);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<Event>> Read(int take, int skip)
    {
        var repository = _unitOfWork.GetRepository<Event>();

        var events = await repository.GetAllAsync(
            predicate: null,
            orderBy: q => q.OrderBy(e => e.Id),
            include: null,
            trackingType: TrackingType.NoTracking,
            ignoreQueryFilters: false,
            ignoreAutoIncludes: false
        );

        var pagedEvents = events.Skip(skip).Take(take).ToList();

        return pagedEvents;
    }

    public async Task Update(Event model)
    {
        var repository = _unitOfWork.GetRepository<Event>();

        var existingEvent = await repository.GetFirstOrDefaultAsync(
            predicate: e => e.Id == model.Id,
            trackingType: TrackingType.NoTracking
        );

        if (existingEvent is null)
        {
            throw new KeyNotFoundException($"Событие с ID {model.Id} не найдено.");
        }

        repository.Update(model);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Event>();

        var model = await repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == id,
            orderBy: null,
            include: null,
            trackingType: TrackingType.Tracking
        );

        if (model is null)
        {
            throw new KeyNotFoundException($"Событие с ID {id} не найдено.");
        }

        repository.Delete(model);
        await _unitOfWork.SaveChangesAsync();
    }

}