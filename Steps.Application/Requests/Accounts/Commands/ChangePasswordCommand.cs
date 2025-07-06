using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Application.Requests.Accounts.Commands;

public record ChangePasswordCommand(ChangePasswordViewModel Model) : IRequest<Result>;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly ISignInManager _signInManager;
    private readonly IUserManager<User> _userManager;

    public ChangePasswordCommandHandler(ISignInManager signInManager, IUserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        
            var currentUser = await _signInManager.GetCurrentUser();
            if (currentUser is null)
            {
                return Result.Fail("Пользователь не найден");
            }

            throw new NotImplementedException();
    }
} 