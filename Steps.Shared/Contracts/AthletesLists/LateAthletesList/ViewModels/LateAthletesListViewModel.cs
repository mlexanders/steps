using Steps.Domain.Entities;
using Steps.Shared.Contracts.AthletesLists.GroupBlock.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Shared.Contracts.AthletesLists.LateAthletesList.ViewModels;

public class LateAthletesListViewModel
{
    public Guid Id { get; set; }
    
    public List<Domain.Entities.AthletesLists.GroupBlock> GroupBlocks { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
}