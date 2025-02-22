using Steps.Shared.Contracts;

namespace Steps.Client.Services.Api;

public static class ApiRoutes
{
    public static class Auth
    {
        public const string Register = "Account/register";
        public const string Login = "Account/login";
        public const string Logout = "Account/logout";
        public const string GetCurrentUser = "Account/GetCurrentUser";
        public const string ConfirmAction = "Account/confirm-action";
    }

    public static class Contests
    {
        private const string BasePath = "Contests";
        public static string Create =>  $"{BasePath}/";
        public static string GetById (Guid contestId) => $"{BasePath}/{contestId}";
        public static string GetPaged(Page page ) => $"{BasePath}/";
        public static string Update = $"{BasePath}/";
        public static string Delete (Guid contestId) => $"{BasePath}/{contestId}";
    }
}