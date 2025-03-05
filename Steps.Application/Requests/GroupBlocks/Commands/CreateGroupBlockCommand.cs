using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.ViewModels;

namespace Steps.Application.Requests.GroupBlocks.Commands;

public record CreateGroupBlockCommand(CreateGroupBlockViewModel Model) : IRequest<Result<GroupBlockViewModel>>;

public class CreateGroupBlockCommandHandler : IRequestHandler<CreateGroupBlockCommand, Result<GroupBlockViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly GroupBlockService _groupBlockService;

    public CreateGroupBlockCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, GroupBlockService groupBlockService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _groupBlockService = groupBlockService;
    }

    public async Task<Result<GroupBlockViewModel>> Handle(CreateGroupBlockCommand request,
        CancellationToken cancellationToken)
    {
        var contest = await _unitOfWork.GetRepository<Contest>()
            .GetFirstOrDefaultAsync(
                predicate: c => c.Id.Equals(request.Model.ContestId),
                trackingType: TrackingType.NoTracking);

        await _groupBlockService.GenerateGroupBlocks(contest, 6);
        // var viewModel = _mapper.Map<GroupBlockViewModel>(entry.Entity);

        return Result<GroupBlockViewModel>.Ok(null!).SetMessage("Список создан");
    }
}