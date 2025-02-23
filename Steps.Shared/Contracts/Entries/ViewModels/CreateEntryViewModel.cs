using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.AthletesLists.EntryAthletesList.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.Entries.ViewModels;

public class CreateEntryViewModel
{
    public int Number { get; set; }
    public bool IsSuccess { get; set; }
    public DateTime SubmissionDate { get; set; }
    
    public Guid ContestId { get; set; }
    public ContestViewModel Contest { get; set; }
    
    public Guid UserId { get; set; }
    public UserViewModel User { get; set; }
    
    public Guid? EntryAthletesListId { get; set; }
    public EntryAthletesListViewModel? EntryAthletesList { get; set; }
}