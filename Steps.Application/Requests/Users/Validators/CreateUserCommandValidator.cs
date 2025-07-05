using FluentValidation;
using Steps.Application.Requests.Users.Commands;

namespace Steps.Application.Requests.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Login).NotNull().NotEmpty().WithMessage("Заполните логин");
            
        }).WithMessage("Все поля должны быть заполнены");
    }
}