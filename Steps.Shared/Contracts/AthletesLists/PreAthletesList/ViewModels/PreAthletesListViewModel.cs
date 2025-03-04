using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.AthletesLists.PreAthletesList.ViewModels;

public class PreAthletesListViewModel
{
    public Guid Id { get; set; }
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
    
    public List<Athlete> Athletes { get; set; }
}