namespace Steps.Shared.Contracts.Entries.ViewModels;

public class CreateEntryViewModel
{
    public Guid ContestId { get; set; }
    public Guid TeamId { get; set; }
    public List<Guid>? AthletesIds { get; set; }
}