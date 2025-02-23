using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Shared.Contracts.AthletesLists.EntryAthletesList.ViewModels;

public class EntryAthletesListViewModel
{
    public Guid Id { get; set; }
    
    public Guid EntryId { get; set; }
    public EntryViewModel Entry { get; set; }
    
    public List<AthleteViewModel> Athletes { get; set; }
}