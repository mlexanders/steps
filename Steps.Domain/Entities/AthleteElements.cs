using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Domain.Entities;

public class AthleteElements : Entity
{
    public string Degree { get; set; }
    public string? Type { get; set; }
    public string AgeCategory { get; set; }
    public string Element1 { get; set; }
    public string Element2 { get; set; }
    public string Element3 { get; set; }
    public string Element4 { get; set; }
    public string Element5 { get; set; }
}