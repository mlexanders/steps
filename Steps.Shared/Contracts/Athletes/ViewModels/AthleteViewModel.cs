using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared.Contracts.AthletesLists.GroupBlock.ViewModels;
using Steps.Shared.Contracts.AthletesLists.PreAthletesList.ViewModels;

namespace Steps.Shared.Contracts.Athletes.ViewModels;

public class AthleteViewModel
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Guid TeamId { get; set; }
    
    public List<Entry>? Entries { get; set; }
    public List<PreAthletesList>? PreAthletesLists { get; set; }
    public List<GroupBlock>? GroupBlocks { get; set; }
}