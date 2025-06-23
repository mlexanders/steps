namespace Steps.Client.Services.Api.Routes;

public interface IAthleteElementsRoutes
{
    string GetAthleteElements (string degree, string ageCategory, string? type);
}