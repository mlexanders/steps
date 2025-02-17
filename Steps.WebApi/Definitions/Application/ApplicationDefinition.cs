using Steps.Application;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Application;

public class ApplicationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.AddApplication();
    }

    public override void Use(WebApplication app)
    {
        app.UseApplication();
    }
}