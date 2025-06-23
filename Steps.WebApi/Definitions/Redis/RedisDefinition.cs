using StackExchange.Redis;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Redis;

public class RedisDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var redisConnection = configuration.GetSection("Redis")["Connection"];
            return ConnectionMultiplexer.Connect(redisConnection);
        });
    }
}