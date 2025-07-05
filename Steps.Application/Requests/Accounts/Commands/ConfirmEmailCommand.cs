using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Shared;

namespace Steps.Application.Requests.Accounts.Commands;

public record ConfirmEmailCommand(string Token) : IRequest<Result>;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
{
    private readonly ISecurityService _securityService;

    public ConfirmEmailCommandHandler(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    public Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
