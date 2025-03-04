using Steps.Domain.Definitions;

namespace Steps.Shared.Contracts.Athletes.ViewModels;

public class AthleteViewModel
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public AthleteType AthleteType { get; set; }
    public Degree Degree { get; set; }
    public Guid TeamId { get; set; }
}