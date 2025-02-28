using MediatR;
using Microsoft.AspNetCore.Mvc;
using Steps.Application.Requests.Athletes.Commands;
using Steps.Application.Requests.Contests.Commands;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

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
    }
}
