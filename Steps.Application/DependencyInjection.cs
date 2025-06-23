using Steps.Application.Events;
using Steps.Application.Events.Base;
using Steps.Application.Events.TestResults;
using Steps.Application.ExceptionsHandling;
using Steps.Application.Services;
using Steps.Utils.AppDefinition;

namespace Steps.Application;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<CommonExceptionHandler>();
        builder.Services.AddTransient<SchedulesService>();
        builder.Services.AddTransient<ScheduleFileService>();
        builder.Services.AddDefinitions(builder, typeof(DependencyInjection));
        
        builder.Services.AddTransient(typeof(IApplicationEventHandler<TestResultCreatedEvent>), typeof(TestResultCreatedApplicationEventHandler));
        builder.Services.AddTransient(typeof(IApplicationEventHandler<UserCreatedEvent>), typeof(UserCreatedEventHandler));

        return builder;
    }
    
    public static void UseApplication(this WebApplication app)
    {
        app.UseDefinitions(typeof(DependencyInjection));
    }
}