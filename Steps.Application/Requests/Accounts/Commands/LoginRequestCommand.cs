using MediatR;
using Steps.Application.Interfaces;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Application.Requests.Accounts.Commands;

public record LoginRequestCommand(LoginRequestViewModel Model) : IRequest<bool>;

public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, bool>
{
    private readonly ISignInManager _signInManager;

    public LoginRequestCommandHandler(ISignInManager signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        await _signInManager.SignInAsync(model.Username, model.Password);
        return true;
    }
}