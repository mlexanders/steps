using FluentValidation;
using Steps.Application.CommonValidators;
using Steps.Application.Requests.TestResults.Commands;

namespace Steps.Application.Requests.TestResults.Validators;

public class CreateTestResultCommandValidator: AbstractValidator<CreateTestResultCommand>
{
    public CreateTestResultCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Scores).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Model.Scores)
                    .SetValidator(new TestScoresValidator<CreateTestResultCommand>());
            }).WithMessage("Проставьте баллы");
            
            RuleFor(x => x.Model.AthleteId).NotEqual(Guid.Empty).WithMessage("Участник не выбран");
            RuleFor(x => x.Model.ContestId).NotEqual(Guid.Empty).WithMessage("Мероприятие не выбрана");
            
        }).WithMessage("Все поля должны быть заполнены");
    }
}