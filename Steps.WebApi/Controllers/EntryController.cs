using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Entries.Commands;
using Steps.Application.Requests.Entries.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Services.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class EntryController : ControllerBase, IEntryService
{
    private readonly IMediator _mediator;

    public EntryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result<EntryViewModel>> Create([FromBody] CreateEntryViewModel entryViewModel)
    {
        return await _mediator.Send(new CreateEntryCommand(entryViewModel));
    }
    
    [HttpGet("{id}")]
    public Task<Result<EntryViewModel>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPatch]
    public Task<Result<Guid>> Update(UpdateEntryViewModel model)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("[action]")]
    public async Task<Result<PaggedListViewModel<EntryViewModel>>> GetPaged([FromQuery] Page page, [FromBody] Specification<Entry>? specification = null)
    {
        return await _mediator.Send(new GetPagedEntriesQuery(page, specification));
    }
    
    [HttpDelete]
    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("Accept-entry")]
    public async Task<Result> AcceptEntry(EntryViewModel entryViewModel)
    {
        return await _mediator.Send(new AcceptEntryCommand(entryViewModel));
    }
}