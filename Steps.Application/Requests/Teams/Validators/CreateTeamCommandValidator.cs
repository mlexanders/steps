using FluentValidation;
using Steps.Application.Requests.Teams.Commands;

namespace Steps.Application.Requests.Teams.Validators;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Team).NotEmpty();
        RuleFor(x => x.Team.Name).Length(1, 1024);
        RuleFor(x => x.Team.OwnerId).NotEqual(Guid.Empty);
    }
}