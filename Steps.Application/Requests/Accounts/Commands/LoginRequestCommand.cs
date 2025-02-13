using MediatR;
using Steps.Application.Interfaces;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Application.Requests.Accounts.Commands;

public record LoginRequestCommand(LoginRequestViewModel Model) : IRequest<Result<bool>>;

public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, Result<bool>>
{
    private readonly ISignInManager _signInManager;

    public LoginRequestCommandHandler(ISignInManager signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<Result<bool>> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        await _signInManager.SignInAsync(model.Username, model.Password);
        return Result<bool>.Ok(true).SetMessage(model.Username);
    }
}