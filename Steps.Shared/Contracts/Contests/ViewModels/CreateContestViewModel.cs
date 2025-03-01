using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Shared.Contracts.Contests.ViewModels;

public class CreateContestViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<User> Judjes { get; set; }
    public List<User> Counters { get; set; }
}