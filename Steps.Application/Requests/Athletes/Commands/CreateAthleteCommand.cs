using AutoMapper;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Calabonga.UnitOfWork;
using Steps.Domain.Definitions;

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
            
            int currentYear = DateTime.Now.Year;
            int birthYear = athlete.BirthDate.Year;

            int age = currentYear - birthYear;

            athlete.AgeCategory = age switch
            {
                >= 15 and <= 18 => AgeCategory.Juniors,         // 15-18 лет (2007-2010)
                >= 12 and <= 14 => AgeCategory.Youth,           // 12-14 лет (2011-2013)
                >= 8 and <= 11 => AgeCategory.BoysGirls,        // 8-11 лет (2014-2017)
                >= 5 and <= 7  => AgeCategory.YoungerChildren,  // 5-7 лет (2018-2020)
                >= 3 and <= 4  => AgeCategory.Baby,             // 3-4 года (2021 и младше)
                _ => throw new InvalidOperationException("Возраст спортсмена не входит ни в одну возрастную категорию.")
            };
            
            //var athleteElementsRepository = _unitOfWork.GetRepository<Domain.Entities.AthleteElements>();

            // var athleteElements = await athleteElementsRepository.GetFirstOrDefaultAsync(
            //             predicate: x => x.AgeCategory == athlete.AgeCategory.ToString()
            //             && x.Degree == athlete.Degree.ToString(),
            //             trackingType: TrackingType.Tracking);

            // athlete.AthleteElementsId = athleteElements.Id;
            // athlete.AthleteElements = athleteElements;

            var entry = await athleteRepository.InsertAsync(athlete, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            var view = _mapper.Map<AthleteViewModel>(entry.Entity);
            return Result<AthleteViewModel>.Ok(view).SetMessage("Спортсмен добавлен!");
        }
    }
}