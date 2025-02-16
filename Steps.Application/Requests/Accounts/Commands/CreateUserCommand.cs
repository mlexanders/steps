using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Domain.Entities;
using Steps.Shared;

namespace Steps.Application.Requests.Accounts.Commands;

public record CreateUserCommand(RegistrationRequestViewModel Model) : IRequest<Result<Guid>>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var user = _mapper.Map<User>(model);

        var createdId = await _userManager.CreateAsync(user, model.Password);

        return Result<Guid>.Ok(createdId).SetMessage("Вы успешно зарегистрированы");
    }
}