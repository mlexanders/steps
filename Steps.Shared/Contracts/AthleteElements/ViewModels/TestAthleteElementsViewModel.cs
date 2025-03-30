using Steps.Domain.Base;

namespace Steps.Shared.Contracts.AthleteElements.ViewModels;

public class TestAthleteElementsViewModel : IHaveId
{
    public Guid Id { get; set; }
    public string Degree { get; set; }
    public string? Type { get; set; }
    public string AgeCategory { get; set; }
    public string Element1 { get; set; }
    public string Element2 { get; set; }
    public string Element3 { get; set; }
    public string Element4 { get; set; }
    public string Element5 { get; set; }
}