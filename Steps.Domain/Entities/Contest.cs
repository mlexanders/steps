using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class Contest : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public List<Entry>? Entries { get; set; }
}