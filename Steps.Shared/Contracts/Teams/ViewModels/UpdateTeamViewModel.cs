using Steps.Domain.Base;

namespace Steps.Shared.Contracts.Teams.ViewModels;

public class UpdateTeamViewModel : IHaveId
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public Guid OwnerId { get; set; }
    public Guid ClubId { get; set; }
}