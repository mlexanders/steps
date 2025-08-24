namespace Steps.Client.Services.Api.Routes;

public interface IAthletesRoutes
{
    string GetRemovedAthletes();
    string GenerateAthleteLabel(string athleteName, string teamName);
}