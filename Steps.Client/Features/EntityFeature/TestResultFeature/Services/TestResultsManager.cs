using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.TestResultFeature.Services;

public class TestResultsManager : EntityManagerBase<TestResult, TestResultViewModel, CreateTestResultViewModel, UpdateTestResultViewModel>
{
    public TestResultsManager(ITestResultsService service) : base(service)
    {
    }
}