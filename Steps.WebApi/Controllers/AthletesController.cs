using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Athletes.Commands;
using Steps.Application.Requests.Athletes.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Services.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AthletesController : ControllerBase, IAthletesService
    {
        private readonly IMediator _mediator;

        public AthletesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<AthleteViewModel>> Create(CreateAthleteViewModel createAthleteViewModel)
        {
            return await _mediator.Send(new CreateAthleteCommand(createAthleteViewModel));
        }
        
        [HttpGet("{id}")]
        public Task<Result<AthleteViewModel>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        
        [HttpPatch]
        public Task<Result<Guid>> Update(UpdateAthleteViewModel model)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("[action]")]
        public async Task<Result<PaggedListViewModel<AthleteViewModel>>> GetPaged([FromQuery] Page page, [FromBody] Specification<Athlete>? specification = null)
        {
            return await _mediator.Send(new GetPagedAthletesQuery(page, specification));
        }
        
        [HttpDelete]
        public Task<Result> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}