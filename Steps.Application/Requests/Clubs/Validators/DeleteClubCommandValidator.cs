using FluentValidation;
using Steps.Application.Requests.Clubs.Commands;

namespace Steps.Application.Requests.Clubs.Validators;

public class DeleteClubCommandValidator : AbstractValidator<DeleteClubCommand>
{
    public DeleteClubCommandValidator()
    {
        RuleFor(x => x.ClubId).NotEmpty().WithMessage("Выберите клуб");
    }
}