using Steps.Domain.Base;

namespace Steps.Domain.Entities.GroupBlocks;

// групповой блок с участниками 
public class GroupBlock : Entity
{
    public Guid ContestId { get; set; }
    public virtual Contest Contest { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public virtual List<ScheduledCell> Schedule { get; set; } = [];
    public virtual List<FinalScheduledCell> FinalSchedule { get; set; } = [];
}


public class ScheduledCell : ScheduledCellBase
{
    public bool IsConfirmed { get; set; }
}

public class FinalScheduledCell : ScheduledCellBase
{
    
}

public abstract class ScheduledCellBase : Entity
{
    public DateTime ExitTime { get; set; }
    public int SequenceNumber { get; set; }

    public Guid AthleteId { get; set; }
    public virtual Athlete Athlete { get; set; } = null!;

    public Guid GroupBlockId { get; set; }
    public virtual GroupBlock GroupBlock { get; set; } = null!;
}