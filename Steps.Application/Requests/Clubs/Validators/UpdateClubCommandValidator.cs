using FluentValidation;
using Steps.Application.Requests.Clubs.Commands;

namespace Steps.Application.Requests.Clubs.Validators;

public class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommand>
{
    public UpdateClubCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Name).Length(2, 1024).WithMessage("Введите имя");
            RuleFor(x => x.Model.OwnerId).NotEmpty().WithMessage("Выберите владельца");
            RuleFor(x => x.Model.Id).NotEmpty().WithMessage("Выберите клуб");
        }).WithMessage("Заполните форму");
       
    }
}