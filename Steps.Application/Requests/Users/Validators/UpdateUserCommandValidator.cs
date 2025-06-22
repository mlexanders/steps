using FluentValidation;
using Steps.Application.Requests.Users.Commands;

namespace Steps.Application.Requests.Users.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Login).NotNull().NotEmpty().WithMessage("Заполните логин");
            RuleFor(x => x.Model.Id).NotNull().NotEmpty().WithMessage("Выберите пользователя");
            
        }).WithMessage("Все поля должны быть заполнены");
    }
}