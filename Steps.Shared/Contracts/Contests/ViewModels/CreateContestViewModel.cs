using Steps.Domain.Definitions;

namespace Steps.Shared.Contracts.Contests.ViewModels;

public class CreateContestViewModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContestType Type { get; set; }
}