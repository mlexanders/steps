using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.Schedules.PreSchedules;

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
        public string GetByTimeInterval(GetContestByInterval criteria)
        {
            return $"{BasePath}/{nameof(IContestsService.GetByTimeInterval)}/{criteria.GetQuery()}";
        }
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

    public class SchedulesService 
    {
        public string GetPagedScheduledCellsByGroupBlockIdQuery => 
            $"Schedules/{nameof(ISchedulesService.GetPagedScheduledCellsByGroupBlockIdQuery)}";
        
        public string Reorder => $"Schedules/{nameof(ISchedulesService.Reorder)}";
        
        public string MarkAthlete = $"Schedules/{nameof(ISchedulesService.MarkAthlete)}";
    }
}