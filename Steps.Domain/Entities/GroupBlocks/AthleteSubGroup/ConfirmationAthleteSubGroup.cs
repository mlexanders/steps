using Steps.Domain.Base;
using Steps.Domain.Entities.GroupBlocks.Base;

namespace Steps.Domain.Entities.GroupBlocks.AthleteSubGroup;

/// <summary>
/// Модель для подтверждения участника в блоке
/// </summary>
/// <inheritdoc>
///     <cref>IAthleteSubGroup</cref>
/// </inheritdoc>
public class ConfirmationAthleteSubGroup : Entity, IAthleteSubGroup<PreSubGroup>
{
    public int SequenceNumber { get; set; }
    public Guid SubGroupId { get; set; }
    public PreSubGroup SubGroup { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;

    /// <summary>
    /// Подтверждение явки участника. True - пришел
    /// </summary>
    public bool IsConfirmed { get; set; }
}