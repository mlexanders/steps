using Steps.Domain.Definitions;

namespace Steps.Shared.Contracts.Athletes.ViewModels;

public class CreateAthleteViewModel
{
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public AthleteType AthleteType { get; set; }
    public AgeCategory AgeCategory { get; set; }
    
    public Guid TeamId { get; set; }
}
