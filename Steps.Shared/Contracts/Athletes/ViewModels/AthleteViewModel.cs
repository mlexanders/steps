using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Shared.Contracts.Athletes.ViewModels;

public class AthleteViewModel : IHaveId
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public AthleteType AthleteType { get; set; }
    public Degree Degree { get; set; }
    public Guid TeamId { get; set; }
    
    public List<Entry>? Entries { get; set; }
    public List<PreAthletesList>? PreAthletesLists { get; set; }
    public List<LateAthletesList>? LateAthletesLists { get; set; }
    public List<GroupBlock>? GroupBlocks { get; set; }
}