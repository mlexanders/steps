using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Client.Services.Api;

public class AthletesService : CrudService<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>, IAthletesService
{
    public AthletesService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.AthletesRoute())
    {
    }
}