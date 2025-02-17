using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Services.WebApi.Services;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.DependencyContainer;

/// <summary>
///     Dependency container definition
/// </summary>
public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<IUserManager<User>, UserManager>();
        services.AddTransient<ISignInManager, SignInManager>();
        // services.AddTransient<IContestManager, ContestManager>();
    }
}