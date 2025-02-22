using Steps.Domain.Base;

namespace Steps.Domain.Entities.AthletesLists;

public class EntryAthletesList : Entity
{
    public Guid EntryId { get; set; }
    public Entry Entry { get; set; }
    
    public List<Athlete> Athletes { get; set; }
}