using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.TestResults.ViewModels;

public class TestResultViewModel : ITestResult, IHaveId
{
    public Guid Id { get; set; }
    public Guid ContestId { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete {  get; set; }
    public List<int> Scores { get; set; } = [];
    public Guid ScoredDegreeId { get; set; }
}