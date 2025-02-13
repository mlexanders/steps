using Steps.Shared;

namespace Steps.Application.ExceptionsHandling.Descriptors;

public abstract class ExceptionDescriptor<TE> where TE : Exception
{
    protected static (Error Error, int StatusCode) GetDescriptionWithStatusCode(string code, string message, int statusCode)
    {
        return (new Error(code, message), statusCode);
    }

    public abstract (Error Error, int StatusCode) GetDescriptionWithStatusCode(TE exception);
}