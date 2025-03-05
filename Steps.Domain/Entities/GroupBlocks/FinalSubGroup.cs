using Steps.Domain.Entities.GroupBlocks.AthleteSubGroup;
using Steps.Domain.Entities.GroupBlocks.Base;

namespace Steps.Domain.Entities.GroupBlocks;

/// <summary>
///   Группа спортсменов с одинаковым ExitTime и GroupBlock
/// </summary>
public class FinalSubGroup : BlockSubGroup<FinalAthleteSubGroup, FinalSubGroup>
{
    public override List<FinalAthleteSubGroup> AthleteBlocks { get; set; } = new();
}