using Steps.Application.Mappers;

namespace Steps.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddAutoMapper(typeof(UserMapperConfiguration));
        return services;
    }
}