using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Contests;

public interface IContestService : ICrudService<ContestViewModel, CreateContestViewModel, UpdateContestViewModel>;