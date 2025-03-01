using Steps.Domain.Base;

namespace Steps.Domain.Entities.AthletesLists;

/// <summary>
/// Список спортсменов, которые опоздали на мероприятие
/// </summary>
public class LateAthletesList : Entity
{
    public List<GroupBlock> GroupBlocks { get; set; } = new List<GroupBlock>();
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
}