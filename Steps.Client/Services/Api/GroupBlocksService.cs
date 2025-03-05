using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.Schedules;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Client.Services.Api;

public class GroupBlocksesService : CrudService<GroupBlock, GroupBlockViewModel, CreateGroupBlockViewModel, UpdateGroupBlockViewModel>, IGroupBlocksService
{
    public GroupBlocksesService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.GroupBlockRoute())
    {
    }
}