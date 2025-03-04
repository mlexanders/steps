using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Domain.Entities;

public class Athlete : Entity
{
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime? ExitTime { get; set; }
    public bool? IsAppeared { get; set; }
    
    public Degree? Degree { get; set; }
    public AthleteType? AthleteType { get; set; }
    
    public Guid TeamId { get; set; }

    public List<Entry> Entries { get; set; } = new List<Entry>();
    public List<PreAthletesList>? PreAthletesLists { get; set; } = new List<PreAthletesList>();
    public List<LateAthletesList> LateAthletesLists { get; set; } = new List<LateAthletesList>();
    public List<GroupBlock>? GroupBlocks { get; set; } = new List<GroupBlock>();
}