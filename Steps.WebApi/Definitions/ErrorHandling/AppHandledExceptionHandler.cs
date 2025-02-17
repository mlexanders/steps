using Steps.Application.Exceptions;
using Steps.Application.Interfaces;
using Steps.Shared;

namespace Steps.Services.WebApi.Definitions.ErrorHandling;

public class AppHandledExceptionHandler : IExceptionHandler
{
    public async Task<(Result content, int statusCode)> GetDescription(Exception exception)
    {
        return exception switch
        {
            AppHandledException handledException => (handledException.Result, handledException.StatusCode),
            _ => throw new ArgumentOutOfRangeException(nameof(exception), exception, null)
        };
    }
}