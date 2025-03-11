using FluentValidation;
using FluentValidation.Validators;

namespace Steps.Application.CommonValidators;

public class TestScoresValidator<T> : PropertyValidator<T, List<int>>
{
    private const int CountScores = 5;
    
    public override bool IsValid(ValidationContext<T> context, List<int> value)
    {
        return value?.Count == CountScores;
    }

    public override string Name { get; }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => $"Должно быть {CountScores} оценок";
}