using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.AthleteElements;
using Steps.Shared.Contracts.AthleteElements.ViewModels;

namespace Steps.Client.Services.Api;

public class TestAthleteElementsService : 
    CrudService<TestAthleteElement, TestAthleteElementsViewModel, CreateAthleteElementsViewModel, UpdateAthleteElementsViewModel>,
    IAthleteElementsService
{
    private readonly IAthleteElementsRoutes _athleteElementsRoutes;
    public TestAthleteElementsService(HttpClientService httpClient, IAthleteElementsRoutes athleteElementsRoutes) : base(httpClient, new ApiRoutes.TestAthleteElementsRoute())
    {
        _athleteElementsRoutes = athleteElementsRoutes;
    }

    public async Task<Result<TestAthleteElementsViewModel>> GetAthleteElements(string degree, string ageCategory, string? type)
    {
        return await HttpClient.GetAsync<Result<TestAthleteElementsViewModel>>(_athleteElementsRoutes.GetAthleteElements(degree, ageCategory, type));
    }
}