using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Steps.Shared.Contracts.GroupBlocks.ViewModels;

public class CreateGroupBlockViewModel
{
    public Guid ContestId { get; set; }
    public int AthletesPerGroup { get; set; }

    public List<Guid> TeamsIds { get; set; } = [];
}