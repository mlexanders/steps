using FluentValidation;
using FluentValidation.Validators;

namespace Steps.Application.CommonValidators;

public class DateTimeKindValidator<T> : PropertyValidator<T, DateTime>
{
    private readonly string _propertyName;
    private const DateTimeKind ValidDateTimeKind = DateTimeKind.Utc;

    public DateTimeKindValidator(string propertyName)
    {
        _propertyName = propertyName;
    }
    
    
    public override bool IsValid(ValidationContext<T> context, DateTime value)
    {
        return value.Kind.Equals(ValidDateTimeKind);
    }
    
    public override string Name => nameof(DateTimeKindValidator<T>);
    
    protected override string GetDefaultMessageTemplate(string errorCode)
        => $"Не указан тип времени {_propertyName}";
    
}