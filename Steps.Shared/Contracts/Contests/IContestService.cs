using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Contests;

public interface IContestsService : ICrudService<ContestViewModel, CreateContestViewModel, UpdateContestViewModel>;