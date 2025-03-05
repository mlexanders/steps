using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Client.Services.Api;

public class AthleteService : CrudService<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>, IAthleteService
{
    public AthleteService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.AthletesRoute())
    {
    }
}