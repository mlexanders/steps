using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Domain.Entities;

public class Contest : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContestType Type { get; set; }

    public List<User>? Judjes { get; set; } = new List<User>();
    public List<User>? Counters { get; set; } = new List<User>();
    
    public List<Entry>? Entries { get; set; } = new List<Entry>();
}