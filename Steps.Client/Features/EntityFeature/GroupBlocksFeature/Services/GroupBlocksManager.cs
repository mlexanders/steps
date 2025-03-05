using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Client.Features.EntityFeature.GroupBlocksFeature.Services;

public class GroupBlocksManager : BaseEntityManager<GroupBlock, GroupBlockViewModel, CreateGroupBlockViewModel, UpdateGroupBlockViewModel>
{
    public GroupBlocksManager(IGroupBlocksService groupBlocksService) : base(groupBlocksService)
    {
    }
}