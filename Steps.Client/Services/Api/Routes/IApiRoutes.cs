using Steps.Shared.Contracts;

namespace Steps.Client.Services.Api.Routes;

public interface IApiRoutes
{
    string BasePath { get; }
    string Create();
    string GetById(Guid id);
    string GetPaged(Page page);
    string Update ();
    string Delete(Guid id);
}