using AutoMapper;
using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Domain.Entities;
using Steps.Shared;

namespace Steps.Application.Requests.Accounts.Commands;

public record RegisterUserCommand(RegistrationViewModel Model) : IRequest<Result<Guid>>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUserManager<User> _userManager;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var user = _mapper.Map<User>(model);

        var created = await _userManager.CreateAsync(user, model.Password);

        return Result<Guid>.Ok(created.Id).SetMessage("Вы успешно зарегистрированы");
    }
}