using Steps.Domain.Base;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Domain.Entities;

public class Entry : Entity
{
    public int Number { get; set; }
    public bool IsSuccess { get; set; }
    public DateTime SubmissionDate { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
    
    public Guid UserId { get; set; } //TODO:название
    public User User { get; set; }
    public List<Athlete>? Athletes { get; set; }
}