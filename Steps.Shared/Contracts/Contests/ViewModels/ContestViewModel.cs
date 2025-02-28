using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Shared.Contracts.Contests.ViewModels;

public class ContestViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<UserViewModel> Judjes { get; set; }
    public List<UserViewModel> Counters { get; set; }
}