using Steps.Domain.Entities.GroupBlocks.Base;
using Steps.Domain.Entities.GroupBlocks.SubGroups;

namespace Steps.Domain.Entities.GroupBlocks;

/// <summary>
///   Группа спортсменов с одинаковым ExitTime и GroupBlock
/// </summary>
public class FinalSubGroup : BlockSubGroup<FinalAthleteSubGroup, FinalSubGroup>
{
    public override List<FinalAthleteSubGroup> AthleteBlocks { get; set; } = new();
}