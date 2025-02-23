using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.AthletesLists.GeneratedAthletesList.ViewModels;
using Steps.Shared.Contracts.AthletesLists.LateAthletesList.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.AthletesLists.GroupBlock.ViewModels;

public class GroupBlockViewModel
{
    public Guid Id { get; set; }
    
    public List<int> Numbers { get; set; }
    public DateTime ExitTime { get; set; }
    
    public List<AthleteViewModel> Athletes { get; set; }
    
    public Guid ContestId { get; set; }
    public ContestViewModel Contest { get; set; }
    
    public Guid? GeneratedAthletesListId { get; set; }
    public GeneratedAthletesListViewModel? GeneratedAthletesList { get; set; }
    
    public Guid? LateAthletesListId { get; set; }
    public LateAthletesListViewModel? LateAthletesList { get; set; }
}