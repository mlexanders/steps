using Steps.Domain.Base;

namespace Steps.Shared.Contracts.Athletes.ViewModels;

public class UpdateAthleteViewModel : IHaveId
{
    public Guid Id { get; set;  }
}