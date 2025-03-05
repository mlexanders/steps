using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Shared.Contracts.Schedules;

public interface IGroupBlocksService : ICrudService<GroupBlock, GroupBlockViewModel, CreateGroupBlockViewModel, UpdateGroupBlockViewModel>;

