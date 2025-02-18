using Steps.Shared;

namespace Steps.Application.Exceptions;

public class AppHandledException : Exception
{
    public Result Result { get; }
    public int StatusCode { get; }

    public AppHandledException(Result result, int statusCode, Exception innerException) 
        : base(innerException.Message, innerException)
    {
        Result = result;
        StatusCode = statusCode;
    }
}