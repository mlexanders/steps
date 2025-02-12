using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class Athlete : Entity
{
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}