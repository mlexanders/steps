using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Shared.Contracts.TestResults;

public interface ITestResultsService
    : ICrudService<TestResult, TestResultViewModel, CreateTestResultViewModel, UpdateTestResultViewModel>;
