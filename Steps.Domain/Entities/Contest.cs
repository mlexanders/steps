using Steps.Domain.Base;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Domain.Entities;

public class Contest : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<User>? Judjes { get; set; } = new List<User>();
    public List<User>? Counters { get; set; } = new List<User>();
    
    public List<Entry>? Entries { get; set; } = new List<Entry>();
    
    public Guid? GeneratedAthletesListId { get; set; }
    public GeneratedAthletesList? GeneratedAthletesList { get; set; }
    
    public Guid? LateAthletesListId { get; set; }
    public LateAthletesList? LateAthletesList { get; set; }
    
    public Guid? PreAthletesListId { get; set; }
    public PreAthletesList? PreAthletesList { get; set; }
    
    public List<GroupBlock>? GroupBlocks { get; set; } = new List<GroupBlock>();
}