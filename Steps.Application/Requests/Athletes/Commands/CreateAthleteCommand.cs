using AutoMapper;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Calabonga.UnitOfWork;

namespace Steps.Application.Requests.Athletes.Commands
{
    public record CreateAthleteCommand(CreateAthleteViewModel Model) : IRequest<Result<Guid>>;

    public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAthleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var athlete = _mapper.Map<Athlete>(model);

            var athleteRepository = _unitOfWork.GetRepository<Athlete>();
            var teamRepository = _unitOfWork.GetRepository<Team>();

            athleteRepository.Insert(athlete);
            
            var team = await teamRepository.GetFirstOrDefaultAsync(t => t.Id == model.TeamId,
                null, null, false);
            
            teamRepository.Update(team);
            
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Ok(athlete.Id).SetMessage("Спортсмен добавлен!");
        }
    }
}