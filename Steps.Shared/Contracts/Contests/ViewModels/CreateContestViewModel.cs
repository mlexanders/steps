using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Shared.Contracts.Contests.ViewModels;

public class CreateContestViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ContestType Type { get; set; }

    public List<Guid> Judjes { get; set; }
    public List<Guid> Counters { get; set; }
}