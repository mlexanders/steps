using Steps.Domain.Base;

namespace Steps.Domain.Entities.AthletesLists;

public class GroupBlock : Entity
{
    public List<int> Numbers { get; set; }
    public DateTime ExitTime { get; set; }
    
    public List<Athlete> Athletes { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
    
    public Guid? GeneratedAthletesListId { get; set; }
    public GeneratedAthletesList? GeneratedAthletesList { get; set; }
    
    public Guid? LateAthletesListId { get; set; }
    public LateAthletesList? LateAthletesList { get; set; }
}