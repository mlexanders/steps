using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.ExceptionsHandling.Descriptors;

internal class BusinessExceptionDescriptor : ExceptionDescriptor<StepsBusinessException>
{
    public override (Result Result, int StatusCode) GetDescriptionWithStatusCode(StepsBusinessException exception)
    {
        
        var message = string.IsNullOrEmpty(exception.Message) ? "Неизвестная ошибка" : exception.Message;
        var statusCode = string.IsNullOrEmpty(exception.Message) ? StatusCodes.Status500InternalServerError : StatusCodes.Status400BadRequest;

        var desc = GetDescriptionWithStatusCode(exception.GetType().Name, message, statusCode);
        return (Result.Fail(desc.Error), desc.StatusCode);
     }
}