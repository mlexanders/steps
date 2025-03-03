using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.Organizer.Services.Contests;

public class ContestManager : BaseEntityManager<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    public ContestManager(IContestsService contestsService) : base(contestsService)
    {
    }
}