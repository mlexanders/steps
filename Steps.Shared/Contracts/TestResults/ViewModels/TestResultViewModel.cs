using Steps.Domain;
using Steps.Domain.Base;

namespace Steps.Shared.Contracts.TestResults.ViewModels;

public class TestResultViewModel : ITestResult, IHaveId
{
    public Guid Id { get; set; }
    public Guid ContestId { get; set; }
    public Guid AthleteId { get; set; }
    public List<int> Scores { get; set; } = [];
}