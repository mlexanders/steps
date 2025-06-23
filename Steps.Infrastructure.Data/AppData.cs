using Microsoft.AspNetCore.Authentication.Cookies;

namespace Steps.Infrastructure.Data;

public static class AppData
{
    public const string PolicyName = "Policy";
    public const string ServiceName = "ServiceName";
    public const string AppVersion = "1.0.0";

    public static class Identity
    {
        public const string CookieName = "Identity";
        public const string LogoutPath = "/api/account/logout";
        public const string LoginPath = "/api/account/login";
        public const string AuthenticationType = CookieAuthenticationDefaults.AuthenticationScheme;
        public static TimeSpan ExpireTimeSpan { get; } = TimeSpan.FromSeconds(1200); //TODO:
    }
}