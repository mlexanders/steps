using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class Team : Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Athlete> Athletes { get; set; } = new List<Athlete>();

    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}