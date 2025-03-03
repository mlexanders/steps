using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class TestResult : Entity
{
    public Guid ContestId { get; set; }
    public virtual Contest Contest { get; set; } = null!;

    public Guid AthleteId { get; set; }
    public virtual Athlete Athlete { get; set; } = null!;

    public Guid JudgeId { get; set; }
    public virtual User Judge { get; set; } = null!;

    public List<int> Scores { get; set; } = [];
}