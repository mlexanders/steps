using FluentValidation;
using Steps.Application.Requests.Accounts.Commands;

namespace Steps.Application.Requests.Accounts.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Name).Length(2, 1024).WithMessage("Введите имя");
            RuleFor(x => x.Model.Login).Length(5, 330).WithMessage("Неккоректный email");
            RuleFor(x => x.Model.Password).Equal(u => u.Model.PasswordConfirm).WithMessage("Пароли не совпадают");
        }).WithMessage("Заполните форму");
       
    }
}