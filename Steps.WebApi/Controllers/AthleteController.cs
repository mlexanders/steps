using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Athletes.Commands;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Services.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AthleteController : ControllerBase, IAthleteService
    {
        private readonly IMediator _mediator;

        public AthleteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<Guid>> Create(CreateAthleteViewModel createAthleteViewModel)
        {
            return await _mediator.Send(new CreateAthleteCommand(createAthleteViewModel));
        }

        public Task<Result<AthleteViewModel>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> Update(UpdateAthleteViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<PaggedListViewModel<AthleteViewModel>>> GetPaged([FromQuery] Page page, Specification<Athlete>? specification = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Result<AthleteViewModel>> ICrudService<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>.Create(CreateAthleteViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}