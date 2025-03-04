using FluentValidation;
using Steps.Application.CommonValidators;
using Steps.Application.Requests.TestResults.Commands;

namespace Steps.Application.Requests.TestResults.Validators;

public class UpdateTestResultCommandValidator: AbstractValidator<UpdateTestResultCommand>
{
    public UpdateTestResultCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Scores).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Model.Scores)
                    .SetValidator(new TestScoresValidator<UpdateTestResultCommand>());
            }).WithMessage("Проставьте баллы");
            
        }).WithMessage("Все поля должны быть заполнены");
    }
}