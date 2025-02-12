
using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.Teams.ViewModels;

public class TeamViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public User? Owner { get; set; }
}