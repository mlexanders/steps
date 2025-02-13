using AutoMapper;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Application.Requests.Accounts.Commands;

public record LoginRequestCommand(LoginRequestViewModel Model) : IRequest<Result<UserViewModel>>;

public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand,Result<UserViewModel>>
{
    private readonly ISignInManager _signInManager;
    private readonly IMapper _mapper;

    public LoginRequestCommandHandler(ISignInManager signInManager, IMapper mapper)
    {
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<Result<UserViewModel>> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var user = await _signInManager.SignInAsync(model.Login, model.Password);
        var viewModel = _mapper.Map<UserViewModel>(user);
        
        return Result<UserViewModel>.Ok(viewModel).SetMessage("Вход выполнен успешно");
    }
}