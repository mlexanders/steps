using Steps.Domain.Base;
using Steps.Domain.Entities.GroupBlocks.Base;

namespace Steps.Domain.Entities.GroupBlocks.SubGroups;

/// <summary>
///   Модель связи участника в финальном блоке
/// </summary>
public class FinalAthleteSubGroup : Entity, IAthleteSubGroup<FinalSubGroup>
{
    public int SequenceNumber { get; set; }
    public Guid SubGroupId { get; set; }
    public FinalSubGroup SubGroup { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
}