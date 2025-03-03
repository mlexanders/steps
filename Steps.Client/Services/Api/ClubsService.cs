using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Services.Api;

public class ClubsService : CrudService<ClubViewModel, CreateClubViewModel, UpdateClubViewModel>, IClubsService
{
    public ClubsService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.ClubsRoute())
    {
    }
}