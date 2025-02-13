using FluentValidation;
using Steps.Shared;

namespace Steps.Application.ExceptionsHandling.Descriptors;

public class ValidationExceptionDescriptor : ExceptionDescriptor<FluentValidation.ValidationException>
{
    public override (Error Error, int StatusCode) GetDescriptionWithStatusCode(ValidationException exception)
    {
        var error = exception.Errors.First();
        return GetDescriptionWithStatusCode(error.ErrorCode, error.ErrorMessage, StatusCodes.Status400BadRequest);
    }
}