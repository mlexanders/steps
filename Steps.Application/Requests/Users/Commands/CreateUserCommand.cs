using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Events;
using Steps.Application.Events.Base;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Application.Requests.Users.Commands;

public record CreateUserCommand(CreateUserViewModel Model) : IRequest<Result<UserViewModel>>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user) => Task.FromResult(user.Role is Role.Organizer);
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IApplicationEventPublisher _eventPublisher;
    private readonly IUserManager<User> _userManager;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IApplicationEventPublisher eventPublisher,
        IUserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
        _userManager = userManager;
    }

    public async Task<Result<UserViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var creatingUserModel = _mapper.Map<User>(request.Model);

            var password = GeneratePassword();
            var user = await _userManager.CreateAsync(creatingUserModel, password);

            await transaction.CommitAsync(cancellationToken);

            var viewModel = _mapper.Map<UserViewModel>(user);
            await _eventPublisher.PublishAsync(new UserCreatedEvent(user, password), cancellationToken);

            return Result<UserViewModel>.Ok(viewModel).SetMessage("Пользователь создан");
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result<UserViewModel>.Fail(e.Message);
        }
    }

    private static string GeneratePassword()
    {
        const int length = 10;
        const string allowed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%*";
        return new string(allowed
            .OrderBy(o => Guid.NewGuid())
            .Take(length)
            .ToArray());
    }
}