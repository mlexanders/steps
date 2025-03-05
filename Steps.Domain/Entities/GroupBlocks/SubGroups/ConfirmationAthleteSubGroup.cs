using Steps.Domain.Base;
using Steps.Domain.Entities.GroupBlocks.Base;

namespace Steps.Domain.Entities.GroupBlocks.SubGroups;

/// <summary>
///   Модель для подтверждения участника в блоке
/// </summary>
public class ConfirmationAthleteSubGroup : Entity, IAthleteSubGroup<PreSubGroup>
{
    public int SequenceNumber { get; set; }
    public Guid SubGroupId { get; set; }
    public PreSubGroup SubGroup { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;

    public bool IsConfirmed { get; set; }
}