using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.Ratings;
using Steps.Shared.Contracts.Schedules.PreSchedulesFeature;

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

    public class ContestsRoute() : BaseApiRoutes("Contests")
    {
        public string GetByTimeInterval(GetContestByInterval criteria) =>
            $"{BasePath}/{nameof(IContestsService.GetByTimeInterval)}/{criteria.GetQuery()}";

        public string CloseCollectingEntries(Guid contestId) =>
            $"{BasePath}/{nameof(IContestsService.CloseCollectingEntries)}/{contestId}";
    }

    public class ClubsRoute() : BaseApiRoutes("Clubs");

    public class TeamsRoute() : BaseApiRoutes("Teams");

    public class AthletesRoute() : BaseApiRoutes("Athletes");

    public class UsersRoute() : BaseApiRoutes("Users"), IUserRoutes
    {
        public string GetJudges(Page page) => $"{BasePath}/GetJudges/{page.GetQuery()}";
        public string GetCounters(Page page) => $"{BasePath}/GetCounters/{page.GetQuery()}";
    }

    public class EntriesRoute() : BaseApiRoutes("Entry"), IEntryRoutes
    {
        public string AcceptEntry(Guid id) => $"{BasePath}/AcceptEntry/{id}";
    }

    public class GroupBlockRoute()
    {
        public string GetTeamsForCreateGroupBlocks(Guid contestId) =>
            $"GroupBlocks/{nameof(IGroupBlocksService.GetTeamsForCreateGroupBlocks)}/{contestId}";

        public string CreateByTeams = $"GroupBlocks/{nameof(IGroupBlocksService.CreateByTeams)}";

        public string DeleteByContestId(Guid contestId) => $"GroupBlocks/{contestId}";

        public string GetById(Guid id) => $"GroupBlocks/{id}";

        public string GetByContestId(Guid contestId) =>
            $"GroupBlocks/{nameof(IGroupBlocksService.GetByContestId)}/{contestId}";

        public string CreateFinalScheduleByGroupBlock(Guid id) =>
            $"GroupBlocks/{nameof(IGroupBlocksService.CreateFinalScheduleByGroupBlock)}/{id}";
    }

    public class PreSchedulesRoute() : ScheduledRoutes("PreSchedules")
    {
        public string Reorder => $"PreSchedules/{nameof(IPreSchedulesService.Reorder)}";

        public string MarkAthlete = $"PreSchedules/{nameof(IPreSchedulesService.MarkAthlete)}";
    }

    public class FinalSchedulesRoute() : ScheduledRoutes("FinalSchedules");

    public class ScheduledRoutes(string basePath)
    {
        private string BasePath { get; } = basePath;

        public string GetPagedScheduled =>
            $"{BasePath}/{nameof(IPreSchedulesService.GetPagedScheduledCellsByGroupBlockIdQuery)}";
    }

    public class TestResultsRoute() : BaseApiRoutes("TestResults");

    public class RatingsRoute 
    {
        public string GetRatingByBlock (Guid id) => $"Ratings/{nameof(IRatingService.GetRatingByBlock)}/{id}";

        public string CreateDiplomas = $"Ratings/{nameof(IRatingService.Complete)}";
    }
}