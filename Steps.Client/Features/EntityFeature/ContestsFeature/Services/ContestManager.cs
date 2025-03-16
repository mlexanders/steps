using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Services;

public class ContestManager : EntityManagerBase<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    private readonly IContestsService _contestsService;

    public ContestManager(IContestsService contestsService) : base(contestsService)
    {
        _contestsService = contestsService;
    }

    public async Task<Result<List<ContestViewModel>>> GetByTimeInterval(DateTime argStart, DateTime argEnd)
    {
        try
        {
            return await _contestsService.GetByTimeInterval(new GetContestByInterval
            {
                Start = argStart,
                End = argEnd
            });
        }
        catch (Exception e)
        {
           return Result<List<ContestViewModel>>.Fail(e.Message);
        }
    }
}