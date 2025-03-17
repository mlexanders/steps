namespace Steps.Shared.Contracts.Schedules.PreSchedules.ViewModels;

public class MarkAthleteViewModel
{
    public Guid GroupBlockId { get; set; }
    public Guid AthleteId { get; set; }
    public bool Confirmation { get; set; }
}