using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Accounts.Commands;
using Steps.Application.Requests.Accounts.Queries;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts;
using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]/[Action]")]
public class AccountController : Controller, IAccountService
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result> Registration([FromBody] RegistrationViewModel model)
    {
        return await _mediator.Send(new RegisterUserCommand(model));
    }

    [HttpPost]
    public async Task<Result<UserViewModel>> Login(LoginViewModel model)
    {
        var loginResult = await _mediator.Send(new LoginRequestCommand(model));
        return loginResult;
    }

    [HttpGet]
    [Authorize]
    public async Task<Result<UserViewModel>> GetCurrentUser()
    {
        return await _mediator.Send(new CurrentUserQuery());
    }


    [HttpPost]
    public async Task<Result> Logout()
    {
        return await _mediator.Send(new LogoutUserCommand());
    }

    [HttpPost]
    public async Task<Result<string>> ChangePassword(ChangePasswordViewModel model)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<Result> ConfirmAction(string token)
    {
        throw new NotImplementedException();
    }
}