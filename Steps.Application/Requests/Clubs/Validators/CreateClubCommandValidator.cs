using FluentValidation;
using Steps.Application.Requests.Clubs.Commands;

namespace Steps.Application.Requests.Clubs.Validators;

public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
{
    public CreateClubCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            //TODO:
        }).WithMessage("Заполните форму");
       
    }
}