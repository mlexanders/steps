using Steps.Shared;

namespace Steps.Application.Interfaces;

public interface IExceptionDescriptor
{
    Task<(Result content, int statusCode)> GetResult(Exception exception);
}