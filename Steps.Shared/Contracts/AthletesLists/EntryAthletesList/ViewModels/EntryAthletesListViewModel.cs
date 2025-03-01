using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Shared.Contracts.AthletesLists.EntryAthletesList.ViewModels;

public class EntryAthletesListViewModel
{
    public Guid Id { get; set; }
    
    public Guid EntryId { get; set; }
    public Entry Entry { get; set; }
    
    public List<Athlete> Athletes { get; set; }
}