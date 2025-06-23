using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Application.Requests.Users.Commands;

public record UpdateUserCommand(UpdateUserViewModel Model) : IRequest<Result<Guid>>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user) => Task.FromResult(user.Role is Role.Organizer);
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Guid>>
{
    private readonly IUserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.Model);
        await _userManager.UpdateAsync(user);

        return Result<Guid>.Ok(user.Id).SetMessage("Данные пользователя обновлены");
    }
}