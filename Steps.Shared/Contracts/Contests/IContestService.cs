using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Contests;

public interface IContestsService : ICrudService<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    Task<Result<List<ContestViewModel>>> GetByTimeInterval(GetContestByInterval criteria);
    Task<Result> CloseCollectingEntries(Guid contestId);
}