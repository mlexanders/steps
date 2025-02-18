using FluentValidation;
using Steps.Application.Requests.Clubs.Queries;
using Steps.Application.Requests.Teams.Commands;

namespace Steps.Application.Requests.Clubs.Validators;

public class GetClubByIdQueryValidator : AbstractValidator<GetClubByIdQuery>
{
    public GetClubByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Выберите клуб");
    }
}