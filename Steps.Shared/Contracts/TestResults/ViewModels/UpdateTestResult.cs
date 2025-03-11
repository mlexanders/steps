using Steps.Domain.Base;

namespace Steps.Shared.Contracts.TestResults.ViewModels;

public class UpdateTestResultViewModel : IHaveId
{
    public Guid Id { get; set; }
    public List<int> Scores { get; set; } = [];
}