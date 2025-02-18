using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Application.Requests.Accounts.Queries;

public record CurrentUserQuery() : IRequest<Result<UserViewModel>>;

public class CurrentUserQueryHandler : IRequestHandler<CurrentUserQuery, Result<UserViewModel>>
{
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public CurrentUserQueryHandler(IMapper mapper, ISecurityService securityService)
    {
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<UserViewModel>> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _securityService.GetCurrentUser();
        var viewModel = _mapper.Map<UserViewModel>(user);
        
        return Result<UserViewModel>.Ok(viewModel);
    }
}