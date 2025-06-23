using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Domain.Entities;

public class Athlete : Entity
{
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public AgeCategory AgeCategory { get; set; }
    public Degree Degree { get; set; }
    public AthleteType AthleteType { get; set; }
    
    public Guid TeamId { get; set; }
    public Team Team { get; set; } = null!;
}