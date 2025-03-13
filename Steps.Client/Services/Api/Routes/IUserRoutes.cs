using Steps.Shared.Contracts;

namespace Steps.Client.Services.Api.Routes
{
    public interface IUserRoutes
    {
        string GetCounters(Page page);
        string GetJudges(Page page);
    }
}
