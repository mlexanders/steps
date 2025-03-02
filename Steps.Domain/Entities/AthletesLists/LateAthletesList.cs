using Steps.Domain.Base;

namespace Steps.Domain.Entities.AthletesLists;

/// <summary>
/// Список спортсменов, которые опоздали на мероприятие
/// </summary>
public class LateAthletesList : Entity
{
    public List<Athlete> Athletes { get; set; } = new List<Athlete>();
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
}