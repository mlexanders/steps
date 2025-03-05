using Steps.Domain.Base;

namespace Steps.Domain.Entities.GroupBlocks;

// групповой блок с участниками 
public class GroupBlock : Entity
{
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public List<PreSubGroup> PreSubGroups { get; set; } = [];
    public List<FinalSubGroup> FinalSubGroups { get; set; } = [];
}
