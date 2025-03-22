using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.AthleteElements;
using Steps.Shared.Contracts.AthleteElements.ViewModels;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Client.Services.Api;

public class AthleteElementsService : 
    CrudService<AthleteElements, AthleteElementsViewModel, CreateAthleteElementsViewModel, UpdateAthleteElementsViewModel>,
    IAthleteElementsService
{
    private readonly IAthleteElementsRoutes _athleteElementsRoutes;
    public AthleteElementsService(HttpClientService httpClient, IAthleteElementsRoutes athleteElementsRoutes) : base(httpClient, new ApiRoutes.AthleteElemensRoute())
    {
        _athleteElementsRoutes = athleteElementsRoutes;
    }

    public async Task<Result<AthleteElementsViewModel>> GetAthleteElements(string degree, string ageCategory, string? type)
    {
        return await HttpClient.GetAsync<Result<AthleteElementsViewModel>>(_athleteElementsRoutes.GetAthleteElements(degree, ageCategory, type));
    }
}