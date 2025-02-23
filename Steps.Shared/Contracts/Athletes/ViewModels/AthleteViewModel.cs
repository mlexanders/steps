using Steps.Shared.Contracts.AthletesLists.EntryAthletesList.ViewModels;
using Steps.Shared.Contracts.AthletesLists.GroupBlock.ViewModels;
using Steps.Shared.Contracts.AthletesLists.PreAthletesList.ViewModels;

namespace Steps.Shared.Contracts.Athletes.ViewModels;

public class AthleteViewModel
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Guid TeamId { get; set; }
    
    public List<EntryAthletesListViewModel>? EntryAthletesLists { get; set; }
    public List<PreAthletesListViewModel>? PreAthletesLists { get; set; }
    public List<GroupBlockViewModel>? GroupBlocks { get; set; }
}