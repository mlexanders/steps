namespace Steps.Domain.Entities.GroupBlocks;

/// <summary>
/// Ячейка финального блока
/// </summary>
public class FinalScheduledCell : ScheduledCellBase
{
    public bool HasScore { get; set; }
}