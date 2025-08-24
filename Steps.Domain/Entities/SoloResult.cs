using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class SoloResult : Entity, ITestResult
{
    public Guid ContestId { get; set; }
    public virtual Contest Contest { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public virtual Athlete Athlete { get; set; } = null!;

    public Guid JudgeId { get; set; }
    public virtual User Judge { get; set; } = null!;
    
    public Guid RatingId { get; set; }
    public Rating Rating { get; set; } = null!;

    // Scores for each element (0-10 points)
    public List<int> Scores { get; set; } = [];
    
    // Comments for specific Solo competition criteria
    public string DanceTechniqueComment { get; set; } = "";
    public string ElementsTechniqueComment { get; set; } = "";
    public string ChoreographyComment { get; set; } = "";
    public string CommunicationComment { get; set; } = "";
    public string GeneralImpressionComment { get; set; } = "";
}
