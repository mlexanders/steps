namespace Steps.Shared.Contracts.Entries.ViewModels;

public class CreateEntryViewModel
{
    public DateTime SubmissionDate { get; set; }
    public Guid ContestId { get; set; }
    public Guid TeamId { get; set; }
    public List<Guid>? AthletesIds { get; set; }
}