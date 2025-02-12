using System.Collections;
using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class Club : Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}