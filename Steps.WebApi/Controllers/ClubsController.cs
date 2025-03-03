using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Clubs.Commands;
using Steps.Application.Requests.Clubs.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
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
    public Task<Result<ClubViewModel>> GetById(Guid clubId)
    {
        return _mediator.Send(new GetClubByIdQuery(clubId));
    }

    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<ClubViewModel>>> GetPaged([FromQuery]Page page, [FromBody] Specification<Club>? specification)
    {
        return _mediator.Send(new GetPagedClubsQuery(page, specification));
    }

    [HttpPatch]
    public Task<Result<Guid>> Update([FromBody] UpdateClubViewModel model)
    {
        return _mediator.Send(new UpdateClubCommand(model));
    }

    [HttpDelete("{clubId:guid}")]
    public Task<Result> Delete(Guid clubId)
    {
        return _mediator.Send(new DeleteClubCommand(clubId));
    }
}