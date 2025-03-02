using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.Organizer.Services.Contest;

public class ContestManager : BaseEntityManager<ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    public ContestManager(IContestsService contestsService) : base(contestsService)
    {
    }
}