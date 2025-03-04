using Steps.Domain.Entities;
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
    
    public List<Athlete> Athletes { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
    
    public Guid? GeneratedAthletesListId { get; set; }
    public Domain.Entities.AthletesLists.GeneratedAthletesList? GeneratedAthletesList { get; set; }
    
    public Guid? LateAthletesListId { get; set; }
    public Domain.Entities.AthletesLists.LateAthletesList? LateAthletesList { get; set; }
}