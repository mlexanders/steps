using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.TestResults.Commands;
using Steps.Application.Requests.TestResults.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class TestResultsController : ControllerBase, ITestResultsService
{
    private readonly IMediator _mediator;

    public TestResultsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Result<TestResultViewModel>> Create([FromBody] CreateTestResultViewModel model)
    {
        return _mediator.Send(new CreateTestResultCommand(model));
    }

    [HttpGet("{id:guid}")]
    public Task<Result<TestResultViewModel>> GetById(Guid id)
    {
        return _mediator.Send(new GetTestResultByIdQuery(id));
    }

    [HttpPatch]
    public Task<Result<Guid>> Update([FromBody] UpdateTestResultViewModel model)
    {
        return _mediator.Send(new UpdateTestResultCommand(model));
    }

    [HttpPost("[action]")]
    public Task<Result<PaggedListViewModel<TestResultViewModel>>> GetPaged([FromQuery] Page page,
        [FromBody] Specification<TestResult>? specification = null)
    {
        return _mediator.Send(new GetPaggedTestResultQuery(page, specification));
    }

    [HttpDelete("{id:guid}")]
    public Task<Result> Delete(Guid id)
    {
        return _mediator.Send(new DeleteTestResultCommand(id));
    }
}