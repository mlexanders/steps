using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.ExceptionsHandling.Descriptors;

public class BusinessExceptionDescriptor : ExceptionDescriptor<StepsBusinessException>
{
    public override (Error Error, int StatusCode) GetDescriptionWithStatusCode(StepsBusinessException exception)
    {
        if (string.IsNullOrEmpty(exception.Message))
            return GetDescriptionWithStatusCode($"{exception.GetType()}", "Неизвестная ошибка",
                StatusCodes.Status500InternalServerError);

        return GetDescriptionWithStatusCode($"{exception.GetType().Name}", exception.Message,
            StatusCodes.Status400BadRequest);
    }
}