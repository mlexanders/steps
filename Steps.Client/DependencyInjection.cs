using Microsoft.AspNetCore.Components.Authorization;
using Steps.Client.Features.EntityFeature.AthleteFeature.Services;
using Steps.Client.Features.EntityFeature.ClubsFeature.Services;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Services;
using Steps.Client.Features.EntityFeature.SchedulesFeature.Services;
using Steps.Client.Features.EntityFeature.TeamsFeature.Services;
using Steps.Client.Features.EntityFeature.TestResultFeature.Services;
using Steps.Client.Features.EntityFeature.UsersFeature.Services;
using Steps.Client.Services.Api;
using Steps.Client.Services.Api.Base;
using Steps.Client.Services.Api.Routes;
using Steps.Client.Services.Api.Scheduled;
using Steps.Client.Services.Authentication;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature;
using Steps.Shared.Contracts.Teams;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.Users;
using static Steps.Client.Services.Api.Routes.ApiRoutes;
using PreSchedulesService = Steps.Client.Services.Api.Scheduled.PreSchedulesService;

namespace Steps.Client;

public static class AddIdentityDependencyInjection
{
    public static void AddDependencyContainer(this IServiceCollection services)
    {
        services.AddTransient<AthleteDialogManager>();
        services.AddTransient<AthleteManager>();
        services.AddTransient<IAthletesService, AthletesService>();
        
        services.AddTransient<ContestDialogManager>();
        services.AddTransient<ContestManager>();
        services.AddTransient<IContestsService, ContestsService>();
        
        services.AddTransient<ClubsDialogManager>();
        services.AddTransient<ClubsManager>();
        services.AddTransient<IClubsService, ClubsService>();
        
        services.AddTransient<UsersDialogManager>();
        services.AddTransient<UsersManager>();
        services.AddTransient<IUsersService, UsersService>();
        
        services.AddTransient<TeamsDialogManager>();
        services.AddTransient<TeamsManager>();
        services.AddTransient<ITeamsService, TeamsService>();
        
        services.AddTransient<EntriesDialogManager>();
        services.AddTransient<EntriesManager>();
        services.AddTransient<IEntryService, EntryService>();

        services.AddSingleton<IUserRoutes, UsersRoute>();
        services.AddSingleton<IEntryRoutes, EntriesRoute>();
        
        services.AddTransient<GroupBlocksDialogManager>();
        services.AddTransient<IGroupBlocksService, GroupBlocksService>();
                
        services.AddTransient<PreSchedulerManager>();
        services.AddTransient<IPreSchedulesService, PreSchedulesService>();
        
        services.AddTransient<FinalSchedulerManager>();
        services.AddTransient<IFinalSchedulesService, FinalSchedulesService>();

        services.AddTransient<TestResultsDialogManager>();
        services.AddTransient<TestResultsManager>();
        services.AddTransient<ITestResultsService, TestResultsService>();
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