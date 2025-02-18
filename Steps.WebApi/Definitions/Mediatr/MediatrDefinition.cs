using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Mediatr;

public class MediatrDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(MediatrDefinition).Assembly));
    }
}