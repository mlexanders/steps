using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.AthleteElements.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.AthleteElements;
using Steps.Shared.Contracts.AthleteElements.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class TestAthleteElementsController : ControllerBase, IAthleteElementsService
{
    private readonly IMediator _mediator;

    public TestAthleteElementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<Result<TestAthleteElementsViewModel>> GetAthleteElements([FromQuery] string degree,
        [FromQuery] string ageCategory, [FromQuery] string? type)
    {
        return await _mediator.Send(new GetAthleteElementsQuery(degree, ageCategory, type));
    }
    
    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<TestAthleteElementsViewModel>>> GetPaged([FromQuery] Page page, [FromBody] Specification<TestAthleteElement>? specification = null)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<Result<TestAthleteElementsViewModel>> Create(CreateAthleteElementsViewModel model)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:Guid}")]
    public Task<Result<TestAthleteElementsViewModel>> GetById(Guid id)
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