using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Entries.Commands;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Entries;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Services.WebApi.Controllers;

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
    public async Task<Result<Guid>> Create([FromBody] CreateEntryViewModel entryViewModel)
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
    
    [HttpGet("paged")]
    public Task<Result<PaggedListViewModel<EntryViewModel>>> GetPaged([FromQuery] Page page, Specification<Entry>? specification = null)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public Task<Result> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("Accept-entry")]
    public async Task<Result> AcceptEntry(Guid entryId)
    {
        return await _mediator.Send(new AcceptEntryCommand(entryId));
    }
    
    [HttpPost("Reject-entry")]
    Task<Result<EntryViewModel>> ICrudService<Entry, EntryViewModel, CreateEntryViewModel, UpdateEntryViewModel>.Create(CreateEntryViewModel model)
    {
        throw new NotImplementedException();
    }
}