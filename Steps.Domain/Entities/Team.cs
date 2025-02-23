using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class Team : Entity
{
    public string Name { get; set; } = null!;
    public string? Address { get; set; }

    public virtual ICollection<Athlete> Athletes { get; set; } = new List<Athlete>();

    public Guid ClubId { get; set; }
    public virtual Club Club { get; set; }
    
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}