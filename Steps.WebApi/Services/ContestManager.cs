using Calabonga.PagedListCore;
using Calabonga.UnitOfWork;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;

namespace Steps.Services.WebApi.Services;

public class ContestManager : IContestManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ContestManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Create(Contest contest)
    {
        var repository = _unitOfWork.GetRepository<Contest>();

        await repository.InsertAsync(contest);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IPagedList<Contest>> Read(int take, int skip)
    {
        var repository = _unitOfWork.GetRepository<Contest>();

        var contests = await repository.GetPagedListAsync(
            predicate: null,
            orderBy: q => q.OrderBy(e => e.Id),
            include: null,
            trackingType: TrackingType.NoTracking,
            ignoreQueryFilters: false,
            ignoreAutoIncludes: false
        );

        return contests;
    }

    public async Task Update(Contest model)
    {
        var repository = _unitOfWork.GetRepository<Contest>();

        var existingContest = await repository.GetFirstOrDefaultAsync(
            predicate: e => e.Id == model.Id,
            trackingType: TrackingType.NoTracking
        );

        if (existingContest is null)
        {
            throw new KeyNotFoundException($"Событие с ID {model.Id} не найдено.");
        }

        repository.Update(model);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Contest>();

        var contest = await repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == id,
            orderBy: null,
            include: null,
            trackingType: TrackingType.Tracking
        );

        if (contest is null)
        {
            throw new KeyNotFoundException($"Событие с ID {id} не найдено.");
        }

        repository.Delete(contest);
        await _unitOfWork.SaveChangesAsync();
    }
}