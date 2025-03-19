using Steps.Domain.Base;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Shared.Contracts.Entries.ViewModels;

public class EntryViewModel : IHaveId
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public bool IsSuccess { get; set; }
    
    public Guid ContestId { get; set; }
    public string ContestName { get; set; }
    
    public Guid TeamId { get; set; }
    public TeamViewModel Team { get; set; } = null!;
    
    public List<Athlete> Athletes { get; set; }
}