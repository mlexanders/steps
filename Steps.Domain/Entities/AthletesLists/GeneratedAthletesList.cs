using Steps.Domain.Base;

namespace Steps.Domain.Entities.AthletesLists;

/// <summary>
/// Сформированный список спортсменов
/// </summary>
public class GeneratedAthletesList : Entity
{
    public List<GroupBlock> GroupBlocks { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
}