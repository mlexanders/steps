using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Ratings;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Ratings;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class RatingsController : ControllerBase, IRatingService
{
    private readonly IMediator _mediator;

    public RatingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]/{groupBlockId:guid}")]
    public Task<Result<RatingViewModel>> GetRatingByBlock(Guid groupBlockId)
    {
        return _mediator.Send(new GetRatingByBlockQuery(groupBlockId));
    }

    [HttpPost("[action]")]
    public Task<Result<DiplomasViewModel>> Complete([FromBody] List<Rating> ratings)
    {
        return _mediator.Send(new CreateDiplomasCommand(ratings));
    }
}

