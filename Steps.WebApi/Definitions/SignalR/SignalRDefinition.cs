using Steps.Shared.Contracts.TestResults;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.SignalR
{
    public class SignalRDefinition : AppDefinition
    {
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddSignalR();
        }

        public override void Use(WebApplication app)
        {
            app.UseRouting();
            app.MapHub<TestResultHub>("/testResultHub");
        }
    }
}
