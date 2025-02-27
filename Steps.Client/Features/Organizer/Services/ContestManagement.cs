using Steps.Shared;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.Organizer.Services;

public class ContestsManagement : BaseEntityManager<ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    public ContestsManagement(IContestService contestService) : base(contestService)
    {
    }
}