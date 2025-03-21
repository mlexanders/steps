using StackExchange.Redis;
using Steps.Application.Interfaces.Base;

namespace Steps.Application.Services;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redisConnection;
    public RedisService(IConnectionMultiplexer redisConnection)
    {
        _redisConnection = redisConnection;
    }
    
    public async Task MarkAthleteAsRemoved(Guid athleteId)
    {
        var db = _redisConnection.GetDatabase();
        await db.SetAddAsync("removedAthletes", athleteId.ToString());
    }
    
    public async Task<List<Guid>> GetRemovedAthletes()
    {
        var db = _redisConnection.GetDatabase();
        var removedAthletes = await db.SetMembersAsync("removedAthletes");
        return removedAthletes.Select(x => Guid.Parse(x)).ToList();
    }
}