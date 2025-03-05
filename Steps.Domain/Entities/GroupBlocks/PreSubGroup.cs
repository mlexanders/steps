using Steps.Domain.Entities.GroupBlocks.AthleteSubGroup;
using Steps.Domain.Entities.GroupBlocks.Base;

namespace Steps.Domain.Entities.GroupBlocks;

/// <summary>
///   Предварительная группа спортсменов с подтверждением участия с одинаковым ExitTime и GroupBlock
/// </summary>
/// <remarks>
/// Может иметь связь с финальной группой, если она была создана
/// </remarks> 
public class PreSubGroup : BlockSubGroup<ConfirmationAthleteSubGroup, PreSubGroup>
{
    public Guid? FinalSubGroupId { get; set; }
    public FinalSubGroup? FinalSubGroup { get; set; }
    public override List<ConfirmationAthleteSubGroup> AthleteBlocks { get; set; } = [];
}