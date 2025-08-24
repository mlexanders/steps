using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.TestResults.ViewModels;

public class SoloResultViewModel : ITestResult, IHaveId
{
    public Guid Id { get; set; }
    public Guid ContestId { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; }
    public List<int> Scores { get; set; } = [];
    
    // Comments for specific Solo competition criteria
    public string DanceTechniqueComment { get; set; } = "";
    public string ElementsTechniqueComment { get; set; } = "";
    public string ChoreographyComment { get; set; } = "";
    public string CommunicationComment { get; set; } = "";
    public string GeneralImpressionComment { get; set; } = "";
    
    public Guid ScoredDegreeId { get; set; }
}
