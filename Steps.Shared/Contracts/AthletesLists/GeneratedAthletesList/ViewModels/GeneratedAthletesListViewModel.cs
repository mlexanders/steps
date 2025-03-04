using Steps.Domain.Entities;
using Steps.Shared.Contracts.AthletesLists.GroupBlock.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.AthletesLists.GeneratedAthletesList.ViewModels;

public class GeneratedAthletesListViewModel
{
    public Guid Id { get; set; }
    
    public List<Domain.Entities.AthletesLists.GroupBlock> GroupBlocks { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
}