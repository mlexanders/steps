namespace Steps.Domain;

public interface ITestResult
{
    Guid ContestId { get; set; }
    Guid AthleteId { get; set; }
    List<int> Scores { get; set; }
}