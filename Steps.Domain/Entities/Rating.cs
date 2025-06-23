using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Domain.Entities;

public class Rating : Entity
{
    public Guid AthleteId { get; set; }
    public virtual Athlete Athlete { get; set; } = null!;

    public Guid TestResultId { get; set; }
    public Guid ContestId { get; set; }
    public Guid GroupBlockId { get; set; }

    public int TotalScore { get; set; }
    public CertificateDegree CertificateDegree { get; set; }
    public AgeCategory AgeCategory { get; set; }
    public bool IsComplete { get; set; }
}