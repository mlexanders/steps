using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Users.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Users;
using Steps.Shared.Contracts.Users.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class UsersController : IUsersService
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Result<UserViewModel>> Create([FromBody] CreateUserViewModel model)
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    public Task<Result<Guid>> Update([FromBody] UpdateUserViewModel model)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    public Task<Result<UserViewModel>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<UserViewModel>>> GetPaged([FromQuery] Page page,
        [FromBody] Specification<User>? specification = null)
    {
        return _mediator.Send(new GetPagedUsersQuery(page, specification));
    }

    [HttpGet("[action]")]
    public Task<Result<PaggedListViewModel<UserViewModel>>> GetJudges([FromQuery] Page page)
    {
        return _mediator.Send(new GetJudgesQuery(page));
    }

    [HttpGet("[action]")]
    public Task<Result<PaggedListViewModel<UserViewModel>>> GetCounters([FromQuery] Page page)
    {
        return _mediator.Send(new GetCountersQuery(page));
    }

    [HttpDelete("{id:guid}")]
    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}