using Steps.Domain.Base;

namespace Steps.Shared.Contracts.GroupBlocks.ViewModels;

public class GroupBlockViewModel : IHaveId
{
    public Guid Id { get; set; }
    public Guid ContestId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public bool IsHaveFinalBlock { get; set; } 
}