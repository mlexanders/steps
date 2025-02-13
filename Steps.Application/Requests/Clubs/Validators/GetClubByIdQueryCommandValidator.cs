using FluentValidation;
using Steps.Application.Requests.Clubs.Queries;

namespace Steps.Application.Requests.Clubs.Validators;

public class GetClubByIdQueryCommandValidator : AbstractValidator<GetClubByIdQuery>
{
    public GetClubByIdQueryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Выберите клуб");
    }
}