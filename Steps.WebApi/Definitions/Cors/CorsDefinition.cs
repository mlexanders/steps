using Steps.Infrastructure.Data;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Cors;

/// <summary>
///     Cors configurations
/// </summary>
public class CorsDefinition : AppDefinition
{
    /// <summary>
    ///     Configure services for current application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AppData.PolicyName, policyBuilder =>
            {
                policyBuilder.WithExposedHeaders();
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.SetIsOriginAllowed(host => true);
                policyBuilder.AllowCredentials();
            });
        });
    }

    public override void Use(WebApplication app)
    {
        app.UseCors(AppData.PolicyName);
    }
}