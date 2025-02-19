namespace Steps.UI.Client;

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
}