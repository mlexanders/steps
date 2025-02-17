using Steps.Application.Behaviors;
using Steps.Application.Behaviors.Base;
using Steps.Utils.AppDefinition;

namespace Steps.Application.Definitions.Mediatr;

public class MediatrDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(MediatrDefinition).Assembly));
        
        services.AddMediatR(cfg =>
        {
            // Важен порядок регистрации 
            cfg.RegisterServicesFromAssemblyContaining<MediatrDefinition>();
            cfg.AddOpenBehavior(typeof(ErrorHandlerBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenRequestPostProcessor(typeof(UnitOfWorkPostProcessor<,>));
        });
    }
}