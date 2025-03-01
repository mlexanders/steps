using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Entries.ViewModels;

public class CreateEntryViewModel
{
    public DateTime SubmissionDate { get; set; }
    
    public Guid ContestId { get; set; }
    
    public Guid UserId { get; set; }
    
    public List<Guid>? AthletesIds { get; set; }
}