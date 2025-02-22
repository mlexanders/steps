using Microsoft.AspNetCore.Components.Authorization;
using Steps.Client.Services.Api;
using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Authentication;
using Steps.Shared.Contracts.Contests;

namespace Steps.Client;

public static class AddIdentityDependencyInjection
{

    public static void AddDependencyContainer(this IServiceCollection services)
    {
        services.AddTransient<IContestService, ContestService>();
    }
    
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddAuthorizationCore();
    
        services.AddScoped<AuthenticationStateProvider, ApplicationAuthenticationStateProvider>();

        services.AddScoped(typeof(CookieHandler));
        services.AddHttpClient(
                "Default",
                opt => opt.BaseAddress = new Uri("http://localhost:5000/api/"))
            .AddHttpMessageHandler<CookieHandler>();

        services.AddScoped<HttpClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            return factory.CreateClient("Default");
        });
        services.AddScoped(typeof(HttpClientService));
        services.AddScoped(typeof(AccountService));
        services.AddScoped(typeof(SecurityService));
    }
}