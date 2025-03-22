using StackExchange.Redis;
using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Client.Services.Api;

public class AthletesService : CrudService<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>, IAthletesService
{
    private readonly IAthletesRoutes _athletesRoutes;
    
    public AthletesService(HttpClientService httpClient, IAthletesRoutes athletesRoutes) : base(httpClient, new ApiRoutes.AthletesRoute())
    {
        _athletesRoutes = athletesRoutes;
    }

    public async Task<Result<List<Guid>>> GetRemovedAthletes()
    {
        return await HttpClient.GetAsync<Result<List<Guid>>>(_athletesRoutes.GetRemovedAthletes());
    }
}