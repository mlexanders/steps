using FluentValidation;
using Steps.Application.Behaviors;
using Steps.Application.Behaviors.Base;
using Steps.Application.ExceptionsHandling;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Application.Services;
using Steps.Utils.AppDefinition;

namespace Steps.Application;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<CommonExceptionHandler>();
        builder.Services.AddTransient<SchedulesService>();
        builder.Services.AddSingleton<IRedisService, RedisService>();
        builder.Services.AddDefinitions(builder, typeof(DependencyInjection));

        return builder;
    }
    
    public static void UseApplication(this WebApplication app)
    {
        app.UseDefinitions(typeof(DependencyInjection));
    }
}