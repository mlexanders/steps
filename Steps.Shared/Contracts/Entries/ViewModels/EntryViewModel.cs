using Steps.Domain.Base;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Entries.ViewModels;

public class EntryViewModel : IHaveId
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public bool IsSuccess { get; set; }
    public DateTime SubmissionDate { get; set; }
    
    public Guid ContestId { get; set; }
    public string ContestName { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public List<Athlete> Athletes { get; set; }
}