namespace Steps.Shared.Contracts.Teams.ViewModels;

public class CreateTeamViewModel
{
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public Guid OwnerId { get; set; }
}