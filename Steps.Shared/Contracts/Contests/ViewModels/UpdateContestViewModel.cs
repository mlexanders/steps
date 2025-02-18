namespace Steps.Shared.Contracts.Contests.ViewModels;

public class UpdateContestViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}