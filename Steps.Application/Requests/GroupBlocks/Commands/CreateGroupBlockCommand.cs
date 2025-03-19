using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.GroupBlocks.Commands;

public record CreateGroupBlocksByTeamsCommand(CreateGroupBlockViewModel Model) : IRequest<Result>;

public class CreateGroupBlocksByTeamsCommandHandler : IRequestHandler<CreateGroupBlocksByTeamsCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly SchedulesService _schedulesService;

    public CreateGroupBlocksByTeamsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, SchedulesService schedulesService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _schedulesService = schedulesService;
    }

    public async Task<Result> Handle(CreateGroupBlocksByTeamsCommand request,
        CancellationToken cancellationToken)
    {
        var contest = await _unitOfWork.GetRepository<Contest>()
                          .GetFirstOrDefaultAsync(
                              predicate: c => c.Id.Equals(request.Model.ContestId),
                              trackingType: TrackingType.NoTracking)
                      ?? throw new StepsBusinessException("Мероприятие не найдено");

        await _schedulesService.GenerateGroupBlocksAndPreCells(contest, request.Model);

        return Result.Ok().SetMessage("Список создан");
    }
}