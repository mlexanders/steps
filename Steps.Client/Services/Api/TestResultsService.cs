using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Services.Api;

public class TestResultsService : CrudService<TestResult, TestResultViewModel, CreateTestResultViewModel, UpdateTestResultViewModel>, ITestResultsService
{
    public TestResultsService(HttpClientService httpClient) : base(httpClient, new ApiRoutes.TestResultsRoute())
    {
    }
}