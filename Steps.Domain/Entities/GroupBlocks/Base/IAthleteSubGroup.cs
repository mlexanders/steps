using Steps.Domain.Base;

namespace Steps.Domain.Entities.GroupBlocks.Base;

/// <summary>
/// Связь спортсмена и подгруппы
/// </summary>
/// <typeparam name="TSubGroup">FinalSubGroup или PreSubGroup</typeparam>
public interface IAthleteSubGroup<TSubGroup> : IHaveId
{
    /// <summary>
    /// Порядковый номер
    /// </summary>
    int SequenceNumber { get; set; }
    
    /// <summary>
    /// Id Подгруппы FinalSubGroup или PreSubGroup
    /// </summary>
    Guid SubGroupId { get; set; }
    
    /// <summary>
    /// FinalSubGroup или PreSubGroup
    /// </summary>
    TSubGroup SubGroup { get; set; }
    
    /// <summary>
    ///  Id участника
    /// </summary>
    Guid AthleteId { get; set; }
    
    /// <summary>
    /// Участник
    /// </summary>
    Athlete Athlete { get; set; }
}