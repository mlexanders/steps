using Steps.Infrastructure.Data;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Identity;

public class AuthorizationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(AppData.Identity.AuthenticationType)
            .AddCookie(options =>
            {
                options.Cookie.Name = AppData.Identity.CookieName;
                options.LoginPath = AppData.Identity.LoginPath;
                options.LogoutPath = AppData.Identity.LogoutPath;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;    
                    context.Response.Headers.AccessControlAllowOrigin = context.Request.Headers.Origin;
                    context.Response.Headers.AccessControlAllowHeaders = "*";
                    context.Response.Headers.AccessControlAllowMethods = "*";
                    context.Response.Headers.AccessControlAllowCredentials = "true";
                    return Task.CompletedTask;
                };
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Используем только HTTPS
                options.ExpireTimeSpan = AppData.Identity.ExpireTimeSpan;
                options.SlidingExpiration = true; // Продление срока действия при активности
            });

        builder.Services.AddAuthorization();
    }

    public override void Use(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}