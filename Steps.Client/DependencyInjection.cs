using Microsoft.AspNetCore.Components.Authorization;
using Steps.Client.Features.Organizer.Services;
using Steps.Client.Features.Organizer.Services.Club;
using Steps.Client.Features.Organizer.Services.Contest;
using Steps.Client.Services.Api;
using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Authentication;
using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Teams;

namespace Steps.Client;

public static class AddIdentityDependencyInjection
{
    public static void AddDependencyContainer(this IServiceCollection services)
    {
        services.AddTransient<ContestDialogManager>();
        services.AddTransient<ContestManager>();
        services.AddTransient<IContestsService, ContestsesService>();
        
        services.AddTransient<ClubsDialogManager>();
        services.AddTransient<ClubsManager>();
        services.AddTransient<IClubsService, ClubsService>();
        
        services.AddTransient<ITeamsService, TeamsService>();
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