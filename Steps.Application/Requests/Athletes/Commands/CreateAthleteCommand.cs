using AutoMapper;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Calabonga.UnitOfWork;

namespace Steps.Application.Requests.Athletes.Commands
{
    public record CreateAthleteCommand(CreateAthleteViewModel Model) : IRequest<Result<AthleteViewModel>>;

    public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, Result<AthleteViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAthleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AthleteViewModel>> Handle(CreateAthleteCommand request,
            CancellationToken cancellationToken)
        {
            var model = request.Model;
            var athlete = _mapper.Map<Athlete>(model);

            var athleteRepository = _unitOfWork.GetRepository<Athlete>();
            var athleteElementsRepository = _unitOfWork.GetRepository<AthleteElements>();

            var athleteElements = await athleteElementsRepository.GetFirstOrDefaultAsync(
                        predicate: x => x.AgeCategory == athlete.AgeCategory.ToString()
                        && x.Degree == athlete.Degree.ToString(),
                        trackingType: TrackingType.Tracking);

            // athlete.AthleteElementsId = athleteElements.Id;
            // athlete.AthleteElements = athleteElements;

            var entry = await athleteRepository.InsertAsync(athlete, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var view = _mapper.Map<AthleteViewModel>(entry.Entity);
            return Result<AthleteViewModel>.Ok(view).SetMessage("Спортсмен добавлен!");
        }
    }
}