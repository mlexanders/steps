using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.Entries.ViewModels;

public class EntryViewModel
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public bool IsSuccess { get; set; }
    public DateTime SubmissionDate { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public List<Athlete> Athletes { get; set; }
}