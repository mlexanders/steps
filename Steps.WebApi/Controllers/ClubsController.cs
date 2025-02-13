using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Clubs.Commands;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class ClubsController : ControllerBase, IClubsService
{
    private readonly IMediator _mediator;

    public ClubsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Result<ClubViewModel>> Create([FromBody] CreateClubViewModel model)
    {
        return _mediator.Send(new CreateClubCommand(model));
    }

    [HttpGet("{clubId:guid}")]
    public Task<Result<ClubViewModel>> GetClubById(Guid clubId)
    {
        throw new NotImplementedException();
    }
}