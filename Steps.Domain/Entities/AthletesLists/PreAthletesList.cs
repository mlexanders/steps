using Steps.Domain.Base;

namespace Steps.Domain.Entities.AthletesLists;

/// <summary>
/// Предварительный список спортсменов
/// </summary>
public class PreAthletesList : Entity
{
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
    
    public List<Athlete> Athletes { get; set; } = new List<Athlete>();
}