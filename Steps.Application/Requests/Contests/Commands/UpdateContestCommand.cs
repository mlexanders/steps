using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Contests.Commands;

public record UpdateContestCommand(UpdateContestViewModel Model) : IRequest<Result<Guid>>;

public class UpdateEventCommandHandler : IRequestHandler<UpdateContestCommand, Result<Guid>>
{
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork;

    public UpdateEventCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateContestCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var repository = _unitOfWork.GetRepository<Contest>();

        var existingContest = await repository.GetFirstOrDefaultAsync(
            predicate: e => e.Id == model.Id,
            trackingType: TrackingType.Tracking
        );

        if (existingContest is null)
        {
            throw new KeyNotFoundException($"Событие с ID {model.Id} не найдено.");
        }

        var updatedModel = _mapper.Map(model, existingContest);

        
        var userRepository = _unitOfWork.GetRepository<User>();

        var judges = await userRepository.GetAllAsync(
            predicate: u => model.JudjesIds.Contains(u.Id) && u.Role == Role.Judge,
            trackingType: TrackingType.Tracking);

        var counters = await userRepository.GetAllAsync(
            predicate: u => model.CountersIds.Contains(u.Id) && u.Role == Role.Counter,
            trackingType: TrackingType.Tracking);

        if (counters.Count != model.CountersIds.Count || judges.Count != model.JudjesIds.Count)
        {
            throw new StepsBusinessException("Неверно выбраны счетчики или судьи");
        }
        
        updatedModel.Judges?.AddRange(judges);
        updatedModel.Counters?.AddRange(counters);
        
        repository.Update(updatedModel);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Ok(updatedModel.Id).SetMessage("Мероприятие успешно обновлено!");
    }
}