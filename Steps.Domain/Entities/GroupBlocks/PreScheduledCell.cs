namespace Steps.Domain.Entities.GroupBlocks;

/// <summary>
/// Ячейка предварительного блока
/// </summary>
public class PreScheduledCell : ScheduledCellBase
{
    public bool IsConfirmed { get; set; }
}