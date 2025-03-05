using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.Teams.ViewModels;

public class TeamViewModel : IHaveId
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public User? Owner { get; set; }
    public Guid ClubId { get; set; }
    public Guid OwnerId { get; set; }
    
    public List<Athlete> Athletes { get; set; }
}