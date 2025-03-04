using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class Entry : Entity
{
    public int Number { get; set; }
    public bool IsSuccess { get; set; }
    public DateTime SubmissionDate { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; } = null!;
    
    public Guid CreatorId { get; set; } 
    public User Creator { get; set; } = null!;
    public List<Athlete> Athletes { get; set; } = null!;
}