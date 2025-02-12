using Serilog;
using Serilog.Events;
using Steps.Services.WebApi.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Logging;

public class LoggerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        
            builder.Host.UseSerilog();
    }
}