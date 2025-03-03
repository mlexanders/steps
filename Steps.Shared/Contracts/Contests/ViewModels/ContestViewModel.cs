using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Shared.Contracts.Contests.ViewModels;

public class ContestViewModel : IHaveId
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<Guid> JudjesIds { get; set; }
    public List<Guid> CountersIds { get; set; }
    public ContestType Type { get; set; }
}