using FluentValidation;
using Steps.Application.CommonValidators;
using Steps.Application.Requests.Clubs.Commands;
using Steps.Application.Requests.Contests.Commands;

namespace Steps.Application.Requests.Contests.Validators;

public class CreateContestCommandValidator : AbstractValidator<CreateContestCommand>
{
    public CreateContestCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Name).Length(2, 1024).WithMessage("Введите название");
            RuleFor(x => x.Model.StartDate).SetValidator(new DateTimeKindValidator<CreateContestCommand>("Дата начала"));
            RuleFor(x => x.Model.EndDate).SetValidator(new DateTimeKindValidator<CreateContestCommand>("Дата начала"));
        }).WithMessage("Заполните форму");
    }
}