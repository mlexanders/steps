using Steps.Domain.Base;

namespace Steps.Domain.Entities.GroupBlocks;

// групповой блок с участниками 
public class GroupBlock : Entity
{
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public List<GroupBlockCell> GroupBlockCells { get; set; } = [];
}


public class GroupBlockCell : Entity
{
    public DateTime ExitTime { get; set; }
    public int SequenceNumber { get; set; }

    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;

    public Guid GroupBlockId { get; set; }
    public GroupBlock GroupBlock { get; set; } = null!;
}
