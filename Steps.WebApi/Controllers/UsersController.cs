using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Users.Queries;
using Steps.Filters.Filters;
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

    public Task<Result<List<UserViewModel>>> GetBy(FilterGroup filter)
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
    
    [HttpGet]
    public Task<Result<PaggedListViewModel<UserViewModel>>> GetPaged([FromQuery] Page page)
    {
        return _mediator.Send(new GetPagedUsersQuery(page));
    }

    [HttpDelete("{id:guid}")]
    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}