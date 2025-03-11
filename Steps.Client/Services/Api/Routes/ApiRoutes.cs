using Steps.Shared.Contracts;

namespace Steps.Client.Services.Api.Routes;

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

    public class ContestsRoute() : BaseApiRoutes("Contests");
    public class ClubsRoute() : BaseApiRoutes("Clubs");
    public class TeamsRoute() : BaseApiRoutes("Teams");
    public class AthletesRoute() : BaseApiRoutes("Athlete");
    public class UsersRoute() : BaseApiRoutes("Users"), IUserRoutes
    {
        public string GetJudges(Page page) => $"{BasePath}/GetJudges/{page.GetQuery()}";
        public string GetCounters(Page page) => $"{BasePath}/GetCounters/{page.GetQuery()}";
    }
    public class EntriesRoute() : BaseApiRoutes("Entry");
}