using Steps.Shared.Contracts.GroupBlocks;

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
    public class UsersRoute() : BaseApiRoutes("Users");
    public class EntriesRoute() : BaseApiRoutes("Entry");

    public class GroupBlockRoute()
    {
        public string GetTeamsForCreateGroupBlocks(Guid contestId) => $"/GroupBlocks/{nameof(IGroupBlocksService.GetTeamsForCreateGroupBlocks)}/{contestId}";
        public string CreateByTeams = $"/GroupBlocks/{nameof(IGroupBlocksService.CreateByTeams)}";
        public string DeleteByContestId(Guid contestId) => $"/GroupBlocks/{nameof(IGroupBlocksService.DeleteByContestId)}/{contestId}";
        public string GetById(Guid id) => $"/GroupBlocks/{id}";
        public string GetByContestId(Guid contestId) => $"/GroupBlocks/{nameof(IGroupBlocksService.GetByContestId)}/{contestId}";
    }
}