using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Accounts.Commands;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Services.WebApi.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class AccountController : Controller, IAccountService
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[Action]")]
    public async Task<Result> Registration([FromBody] RegistrationRequestViewModel model)
    {
       var id = await _mediator.Send(new CreateUserCommand(model));
       return Result<Guid>.Success(id).SetMessage($"User with ID {id} was created");
    }

    [HttpPost("[Action]")]
    public async Task<Result> Login(LoginRequestViewModel model)
    {
        
        var loginResult = await _mediator.Send(new LoginRequestCommand(model));
        
        return loginResult ? Result.Success("Вход выполнен") : Result<bool>.Failure();
    }


    [HttpPost("[Action]")]
    public async Task<Result> Logout()
    {
        throw new NotImplementedException();
    }

    [HttpPost("[Action]")]
    public async Task<Result<string>> ChangePassword(ChangePasswordRequestViewModel model)
    {
        throw new NotImplementedException();
    }

    [HttpPost("[Action]")]
    public async Task<Result> ConfirmAction(string token)
    {
        throw new NotImplementedException();
    }
}