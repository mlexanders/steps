using Steps.Domain.Base;

namespace Steps.Shared.Contracts.TestResults.ViewModels;

public class UpdateSoloResultViewModel : IHaveId
{
    public Guid Id { get; set; }
    public List<int> Scores { get; set; } = [];
    
    // Comments for specific Solo competition criteria
    public string DanceTechniqueComment { get; set; } = "";
    public string ElementsTechniqueComment { get; set; } = "";
    public string ChoreographyComment { get; set; } = "";
    public string CommunicationComment { get; set; } = "";
    public string GeneralImpressionComment { get; set; } = "";
}
