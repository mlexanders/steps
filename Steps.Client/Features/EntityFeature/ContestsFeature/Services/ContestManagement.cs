using Steps.Domain.Entities;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Services;

public class
    ContestManager : BaseEntityManager<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    public ContestManager(IContestsService contestsService) : base(contestsService)
    {
    }
}