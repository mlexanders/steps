using Steps.Domain.Base;

namespace Steps.Shared.Contracts.GroupBlocks.ViewModels;

public class UpdateGroupBlockViewModel : IHaveId
{
    public Guid Id { get; set; }
}