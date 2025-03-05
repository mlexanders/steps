using Steps.Domain.Base;
using Steps.Domain.Entities.GroupBlocks.Base;

namespace Steps.Domain.Entities.GroupBlocks.AthleteSubGroup;

/// <summary>
///   Модель связи участника в финальном блоке
/// </summary>
/// <inheritdoc>
///     <cref>IAthleteSubGroup</cref>
/// </inheritdoc>
public class FinalAthleteSubGroup : Entity, IAthleteSubGroup<FinalSubGroup>
{
    public int SequenceNumber { get; set; }
    public Guid SubGroupId { get; set; }
    public FinalSubGroup SubGroup { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
}