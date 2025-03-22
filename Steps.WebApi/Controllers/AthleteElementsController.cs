using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.AthleteElements.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.AthletesElements;
using Steps.Shared.Contracts.AthletesElements.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AthleteElementsController : ControllerBase, IAthleteElementsService
{
    private readonly IMediator _mediator;

    public AthleteElementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<Result<AthleteElementsViewModel>> GetAthleteElements([FromQuery] string degree,
        [FromQuery] string ageCategory, [FromQuery] string? type)
    {
        return await _mediator.Send(new GetAthleteElementsQuery(degree, ageCategory, type));
    }
    
    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<AthleteElementsViewModel>>> GetPaged([FromQuery] Page page, [FromBody] Specification<AthleteElements>? specification = null)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<Result<AthleteElementsViewModel>> Create(CreateAthleteElementsViewModel model)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:Guid}")]
    public Task<Result<AthleteElementsViewModel>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPatch]
    public Task<Result<Guid>> Update(UpdateAthleteElementsViewModel model)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:Guid}")]
    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}