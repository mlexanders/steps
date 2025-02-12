using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Domain.Entities;

namespace Steps.Application.Requests.Accounts.Commands;

public record CreateUserCommand (RegistrationRequestViewModel Model) : IRequest<Guid>;


public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var user = _mapper.Map<User>(model);

        return await _userManager.CreateAsync(user, model.Password);
    }
}