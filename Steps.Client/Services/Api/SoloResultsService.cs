using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Services.Api;

public class SoloResultsService : CrudService<SoloResult, SoloResultViewModel, CreateSoloResultViewModel, UpdateSoloResultViewModel>, ISoloResultsService
{
    public SoloResultsService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.SoloResultsRoute())
    {
    }
}
