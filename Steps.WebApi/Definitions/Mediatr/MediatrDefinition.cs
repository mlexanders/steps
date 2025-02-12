using FluentValidation;
using MediatR;
using Steps.Application;
using Steps.Application.Behaviors.Base;
using Steps.Services.WebApi.Utils.AppDefinition;
namespace Steps.Services.WebApi.Definitions.Mediatr;

public class MediatrDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddApplication();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    }
}