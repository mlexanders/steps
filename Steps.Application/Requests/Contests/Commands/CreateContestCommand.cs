using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
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
        List<User> judges = new List<User>();
        List<User> counters = new List<User>();
        
        var model = request.Model;
        var contest = _mapper.Map<Contest>(model);

        var repository = _unitOfWork.GetRepository<Contest>();
        var userRepository = _unitOfWork.GetRepository<User>();
        
        foreach (var id in request.Model.Judjes)
        {
            var judge = await userRepository.GetFirstOrDefaultAsync(c => c.Id == id,
                null,
                null,
                TrackingType.Tracking,
                false,
                false);

            judges.Add(judge);
        }
        
        foreach (var id in request.Model.Counters)
        {
            var counter = await userRepository.GetFirstOrDefaultAsync(c => c.Id == id,
                null,
                null,
                TrackingType.Tracking,
                false,
                false);

            counters.Add(counter);
        }

        contest.Judges = judges;
        contest.Counters = counters;
        
        var entry = await repository.InsertAsync(contest, cancellationToken);
        
        
        
        await _unitOfWork.SaveChangesAsync();

        var viewModel = _mapper.Map<ContestViewModel>(entry.Entity);

        return Result<ContestViewModel>.Ok(viewModel).SetMessage("Мероприятие успешно создано!");
    }
}