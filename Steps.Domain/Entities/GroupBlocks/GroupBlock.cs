using Steps.Domain.Base;

namespace Steps.Domain.Entities.GroupBlocks;


/// <summary>
///  Групповой блок с участниками
/// </summary>
public class GroupBlock : Entity
{
    public Guid ContestId { get; set; }
    public virtual Contest Contest { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public virtual List<PreScheduledCell> PreSchedule { get; set; } = [];
    public virtual List<FinalScheduledCell> FinalSchedule { get; set; } = [];
}