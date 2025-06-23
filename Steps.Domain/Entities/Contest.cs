using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Domain.Entities;

public class Contest : Entity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContestType Type { get; set; }
    public ContestStatus Status { get; set; }

    public List<User>? Judges { get; set; } = [];
    public List<User>? Counters { get; set; } = [];
    
    public List<Entry>? Entries { get; set; } = [];
    
    public Guid? PreScheduleFileId { get; set; }
    public ScheduleFile? PreScheduleFile { get; set; }
    
    public Guid? FinalScheduleFileId { get; set; }
    public ScheduleFile? FinalScheduleFile { get; set; }
}