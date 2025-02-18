using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Shared.Exceptions;

namespace Steps.Application.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ISecurityService _securityService;

    public AuthorizationBehavior(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not IRequireAuthorization authRequest) return await next();
        
        var user = await _securityService.GetCurrentUser();
        if (user == null)
        {
            throw new AppAccessDeniedException();
        }
        
        if (! await authRequest.CanAccess(user))
        {
            throw new AppAccessDeniedException();
        }

        return await next();
    }
}