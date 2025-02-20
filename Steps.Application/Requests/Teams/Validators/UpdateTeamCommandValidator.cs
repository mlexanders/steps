using FluentValidation;
using Steps.Application.Requests.Teams.Commands;

namespace Steps.Application.Requests.Teams.Validators;

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamCommandValidator()
    {
        RuleFor(x => x.Model).NotNull().DependentRules(() =>
        {
            RuleFor(x => x.Model.Name).Length(1, 1024).WithMessage("Введите название команды");
            RuleFor(x => x.Model.OwnerId).NotEqual(Guid.Empty).WithMessage("Тренер команды не выбран");
            RuleFor(x => x.Model.ClubId).NotEqual(Guid.Empty).WithMessage("Команда не выбрана");
        }).WithMessage("Свойства команды должны быть заполнены");
    }
}