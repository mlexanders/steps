using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Requests.Contests.Commands;

public record CreateContestCommand(CreateContestViewModel Model) : IRequest<Result<ContestViewModel>>;

public class CreateEventCommandHandler : IRequestHandler<CreateContestCommand, Result<ContestViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ContestViewModel>> Handle(CreateContestCommand request,
        CancellationToken cancellationToken)
    {
        var model = request.Model;
        var contest = _mapper.Map<Contest>(model);

        var repository = _unitOfWork.GetRepository<Contest>();
        var userRepository = _unitOfWork.GetRepository<User>();

        if (model.Judjes is not null && model.Judjes.Count > 0)
        {
            var addedJudje = await userRepository.GetAllAsync(
                predicate: u => model.Judjes.Contains(u.Id) && u.Role == Role.Judge,
                trackingType: TrackingType.Tracking);
            
            contest.Judges = addedJudje.ToList();
        }
        
        if (model.Counters is not null && model.Counters.Count > 0)
        {
            var addedCounters = await userRepository.GetAllAsync(
                predicate: u => model.Counters.Contains(u.Id) && u.Role == Role.Counter,
                trackingType: TrackingType.Tracking);
            
            contest.Counters = addedCounters.ToList();
        }

        var entry = await repository.InsertAsync(contest, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync();

        var viewModel = _mapper.Map<ContestViewModel>(entry.Entity);

        return Result<ContestViewModel>.Ok(viewModel).SetMessage("Мероприятие успешно создано!");
    }
}