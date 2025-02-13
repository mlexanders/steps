using FluentValidation;
using Steps.Application.Requests.Teams.Commands;

namespace Steps.Application.Requests.Teams.Validators;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Team).NotNull();
        RuleFor(x => x.Team.Name).Length(1, 1024).WithMessage("Введите название команды");
        RuleFor(x => x.Team.OwnerId).NotEqual(Guid.Empty).WithMessage("Тренер команды не выбран");
    }
}