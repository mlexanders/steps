using FluentValidation;
using Steps.Application.Requests.Accounts.Commands;

namespace Steps.Application.Requests.Accounts.Validators;

public class LoginRequestCommandValidator : AbstractValidator<LoginRequestCommand>
{
    public LoginRequestCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Login).NotEmpty().WithMessage("Введите логин");
            RuleFor(x => x.Model.Password).NotEmpty().WithMessage("Введите пароль");
        }).WithMessage("Заполните форму");
       
    }
}