using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Shared;

namespace Steps.Application.Requests.Accounts.Commands;

public record LogoutUserCommand() : IRequest<Result>;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Result>
{
    private readonly ISignInManager _signInManager;

    public LogoutUserCommandHandler(ISignInManager signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOut();
        return Result.Ok();
    }
}