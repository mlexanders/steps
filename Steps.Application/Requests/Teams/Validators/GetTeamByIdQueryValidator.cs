using FluentValidation;
using Steps.Application.Requests.Teams.Queries;

namespace Steps.Application.Requests.Teams.Validators;


public class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
{
    public GetTeamByIdQueryValidator()
    {
        RuleFor(x => x.TeamId).NotEmpty().WithMessage("Команда не выбрана");
    }
}