using Steps.Domain.Base;

namespace Steps.Domain.Entities.GroupBlocks.Base;


/// <summary>
/// Связь спортсмена и подгруппы
/// </summary>
/// <typeparam name="TSubGroup">FinalSubGroup или PreSubGroup</typeparam>
public interface IAthleteSubGroup<TSubGroup> : IHaveId
{
    int SequenceNumber { get; set; }
    Guid SubGroupId { get; set; }
    TSubGroup SubGroup { get; set; }

    Guid AthleteId { get; set; }
    Athlete Athlete { get; set; }
}