using Steps.Shared.Contracts;

namespace Steps.Client.Services.Api.Routes;

public class BaseApiRoutes(string basePath) : IApiRoutes
{
    public string BasePath { get; } = basePath;

    public string Create() => $"{BasePath}/";

    public string GetById(Guid id) => $"{BasePath}/{id}";

    public string GetPaged(Page page)=> $"{BasePath}/{page.GetQuery()}";

    public string Update() => $"{BasePath}/";
    public string Delete(Guid id) => $"{BasePath}/{id}"; 
}