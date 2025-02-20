using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Common;

/// <summary>
///     AspNetCore common configuration
/// </summary>
public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers();

        services.AddLocalization();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
        services.AddMemoryCache();
    }

    public override void Use(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapControllers();
    }
}