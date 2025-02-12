using Steps.Infrastructure.Data;
using Steps.Services.WebApi.Utils.AppDefinition;

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