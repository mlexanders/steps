using FluentValidation;
using Steps.Application.Requests.Clubs.Commands;

namespace Steps.Application.Requests.Clubs.Validators;

public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
{
    public CreateClubCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Name).Length(2, 1024).WithMessage("Введите имя");
            RuleFor(x => x.Model.OwnerId).NotEmpty().WithMessage("Выберите владельца");
        }).WithMessage("Заполните форму");
    }
}