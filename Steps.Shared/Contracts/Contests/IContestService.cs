using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Contests;

public interface IContestsService : ICrudService<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    Task<Result> GenerateGroupBlocks(Guid contestId, int athletesCount);
    Task<Result> CheckAthlete(Guid athleteId, Guid contestId, bool isAppeared);
    Task<Result> CloseCollectingEntries(Guid contestId);
}