using Calabonga.PagedListCore;
using Calabonga.UnitOfWork;
using Steps.Application.Interfaces;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application;

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

        var userRepository = _unitOfWork.GetRepository<User>();

        var judjes = await userRepository.GetAllAsync(
            predicate: j => j.Role == Role.Judje,
            orderBy: null,
            include: null,
            disableTracking: false,
            ignoreQueryFilters: false
        );
        
        var counters = await userRepository.GetAllAsync(
            predicate: j => j.Role == Role.Counter,
            orderBy: null,
            include: null,
            disableTracking: false,
            ignoreQueryFilters: false
        );
        
        contest.Counters = counters?.ToList();
        contest.Judjes = judjes?.ToList();

        await repository.InsertAsync(contest);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IPagedList<ContestViewModel>> Read(Page page)
    {
        var repository = _unitOfWork.GetRepository<Contest>();

        var contests = await repository.GetPagedListAsync(
            // selector: (club) => _mapper.Map<ViewModel???>(club),
            pageIndex: page.PageIndex,
            pageSize: page.PageSize,
            // cancellationToken: cancellationToken,
            trackingType: TrackingType.NoTracking);

        throw new NotImplementedException();
        // return contests;
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