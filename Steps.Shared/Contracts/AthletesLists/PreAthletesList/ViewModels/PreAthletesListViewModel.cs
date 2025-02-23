using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.AthletesLists.PreAthletesList.ViewModels;

public class PreAthletesListViewModel
{
    public Guid ContestId { get; set; }
    public ContestViewModel Contest { get; set; }
    
    public List<AthleteViewModel> Athletes { get; set; }
}