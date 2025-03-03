using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Application.Requests.Contests.Commands;

public record CreateContestCommand (CreateContestViewModel Model) : IRequest<Result<ContestViewModel>>;

public class CreateEventCommandHandler : IRequestHandler<CreateContestCommand, Result<ContestViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ContestViewModel>> Handle(CreateContestCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var contest = _mapper.Map<Contest>(model);

        var repository = _unitOfWork.GetRepository<Contest>();

        var entry = await repository.InsertAsync(contest, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        
        var viewModel = _mapper.Map<ContestViewModel>(entry.Entity);
        
        return Result<ContestViewModel>.Ok(viewModel).SetMessage("Мероприятие успешно создано!");
    }
}