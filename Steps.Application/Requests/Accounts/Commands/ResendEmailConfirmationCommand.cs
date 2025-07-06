using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Shared;

namespace Steps.Application.Requests.Accounts.Commands;

public record ResendEmailConfirmationCommand : IRequest<Result>;

public class ResendEmailConfirmationCommandHandler : IRequestHandler<ResendEmailConfirmationCommand, Result>
{
    private readonly ISignInManager _signInManager;
    private readonly INotificationService _notificationService;

    public ResendEmailConfirmationCommandHandler(ISignInManager signInManager, INotificationService notificationService)
    {
        _signInManager = signInManager;
        _notificationService = notificationService;
    }

    public async Task<Result> Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _signInManager.GetCurrentUser();
        if (currentUser == null)
        {
            return Result.Fail("Пользователь не найден");
        }
        
        throw new NotImplementedException();
    }
    
} 