namespace Steps.Application.Interfaces.Base;

public interface IRedisService
{
    Task MarkAthleteAsRemoved(Guid athleteId);
    Task<List<Guid>> GetRemovedAthletes();
}