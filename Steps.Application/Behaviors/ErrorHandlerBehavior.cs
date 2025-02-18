using MediatR;
using Steps.Application.Exceptions;
using Steps.Application.ExceptionsHandling;
using Steps.Shared;

namespace Steps.Application.Behaviors;

public class ErrorHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : Result
    where TRequest : notnull
{
    private readonly CommonExceptionHandler _exceptionHandler;

    public ErrorHandlerBehavior(CommonExceptionHandler exceptionHandler)
    {
        _exceptionHandler = exceptionHandler;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await next();
            return result;
        }
        catch (Exception e)
        {
            var description = await _exceptionHandler.GetDescription(e);
            throw new AppHandledException(description.content, description.statusCode, e);
        }
    }
}