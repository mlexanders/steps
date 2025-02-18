using FluentValidation;
using Steps.Application.Requests.Teams.Commands;

namespace Steps.Application.Requests.Teams.Validators;

public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
{
    public DeleteTeamCommandValidator()
    {
        RuleFor(x => x.TeamId).NotEmpty().WithMessage("Команда не выбрана");
    }
}