namespace Steps.UI.Client.Services;

public static class ApiRoutes
{
    public static class Auth
    {
        public const string Register = "Account/register";
        public const string Login = "Account/login";
        public const string Logout = "Account/logout";
        public const string GetCurrentUser = "Account/current-user";
        public const string ConfirmAction = "Account/confirm-action";
    }
}