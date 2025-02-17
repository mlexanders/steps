using FluentValidation;
using Steps.Application.ExceptionsHandling;

namespace Steps.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddAutoMapper(typeof(DependencyInjection));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient<CommonExceptionHandler>(); 
        return services;
    }
}