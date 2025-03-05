using Microsoft.AspNetCore.Components.Authorization;
using Steps.Client.Features.EntityFeature.AthleteFeature.Services;
using Steps.Client.Features.EntityFeature.ClubsFeature.Services;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Client.Features.EntityFeature.TeamsFeature.Services;
using Steps.Client.Features.EntityFeature.UsersFeature.Services;
using Steps.Client.Services.Api;
using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Authentication;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.Users;

namespace Steps.Client;

public static class AddIdentityDependencyInjection
{
    public static void AddDependencyContainer(this IServiceCollection services)
    {
        services.AddTransient<AthleteDialogManager>();
        services.AddTransient<AthleteManager>();
        services.AddTransient<IAthleteService, AthleteService>();
        
        services.AddTransient<ContestDialogManager>();
        services.AddTransient<ContestManager>();
        services.AddTransient<IContestsService, ContestsesService>();
        
        services.AddTransient<ClubsDialogManager>();
        services.AddTransient<ClubsManager>();
        services.AddTransient<IClubsService, ClubsService>();
        
        services.AddTransient<UsersDialogManager>();
        services.AddTransient<UsersManager>();
        services.AddTransient<IUsersService, UsersService>();
        
        services.AddTransient<TeamsDialogManager>();
        services.AddTransient<TeamsManager>();
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