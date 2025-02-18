using Steps.Shared;

namespace Steps.Application.Interfaces;

public interface IExceptionHandler
{
    Task<(Result content, int statusCode)> GetDescription(Exception exception);
}