using Steps.Domain.Base;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Domain.Entities;

public class Athlete : Entity
{
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime? ExitTime { get; set; }
    public bool? IsAppeared { get; set; }
    public Guid TeamId { get; set; }

    public List<Entry> Entries { get; set; } = new List<Entry>();
    public List<EntryAthletesList>? EntryAthletesLists { get; set; } = new List<EntryAthletesList>();
    public List<PreAthletesList>? PreAthletesLists { get; set; } = new List<PreAthletesList>();
    public List<GroupBlock>? GroupBlocks { get; set; } = new List<GroupBlock>();
}