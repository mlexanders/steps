using Steps.Application.Interfaces;
using Steps.Services.WebApi.Middleware;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.ErrorHandling;

/// <summary>
///  Error handling 
/// </summary>
public class ErrorHandlingDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IExceptionHandler, AppHandledExceptionHandler>();
    }

    public override void Use(WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}