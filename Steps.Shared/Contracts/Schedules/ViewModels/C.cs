using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Schedules.ViewModels;

public class ScheduleContest
{
    public Guid ContestId { get; set; }
    public ContestViewModel Contest { get; set; }

    public List<GroupBlockViewModel> GroupBlocks { get; set; }
}

public class GroupBlockViewModel
{
    public Guid GroupBlockId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<AthleteGroupBlockViewModel> Athletes { get; set; }
}

public class AthleteGroupBlockViewModel
{
    public Guid GroupBlockId { get; set; }
    public DateTime ExitTime { get; set; }
    public AthleteViewModel Athlete { get; set; }
}