using Steps.Domain;

namespace Steps.Shared.Contracts.TestResults.ViewModels;

public class CreateTestResultViewModel : ITestResult
{
    public Guid ContestId { get; set; }
    public Guid AthleteId { get; set; }
    public List<int> Scores { get; set; } = [];
}