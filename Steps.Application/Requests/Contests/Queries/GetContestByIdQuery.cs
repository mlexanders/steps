using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Contests.Queries;

public record GetContestByIdQuery(Guid ContestId) : IRequest<Result<ContestViewModel>>;

public class GetContestByIdQueryHandler : IRequestHandler<GetContestByIdQuery, Result<ContestViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly GroupBlockService _groupBlockService;

    public GetContestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, GroupBlockService groupBlockService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _groupBlockService = groupBlockService;
    }


    public async Task<Result<ContestViewModel>> Handle(GetContestByIdQuery request, CancellationToken cancellationToken)
    {
        var contest1 = await _unitOfWork.GetRepository<Contest>()
            .GetFirstOrDefaultAsync(
                predicate: c => c.Id.Equals(request.ContestId),
                include: x => x.Include(a => a.Judges).Include(a => a.Counters),
                orderBy: c => c.OrderByDescending(o => o.StartDate),
                trackingType: TrackingType.NoTracking);

        // await _groupBlockService.GeneratePreGroupBlocks(contest1, 4);
        var blocks = await _groupBlockService.GetGroupBlocks(contest1);
        var block = blocks.FirstOrDefault();
        
        await _groupBlockService.GenerateFinalGroupBlocks(block.Id);
        
         // await _groupBlockService.GeneratePreGroupBlocks(contest1, 5);
        
        var contest = await _unitOfWork.GetRepository<Contest>()
            .GetFirstOrDefaultAsync(
                predicate: c => c.Id.Equals(request.ContestId),
                selector: c => _mapper.Map<ContestViewModel>(c),
                include: x => x.Include(a => a.Judges).Include(a => a.Counters),
                orderBy: c => c.OrderByDescending(o => o.StartDate),
                trackingType: TrackingType.NoTracking);

        if (contest == null) throw new AppNotFoundException("Мероприятие не найдено");

        return Result<ContestViewModel>.Ok(contest);
    }
}


// "0195620d-fceb-729a-893b-7e57567929b9",
// "0195620e-111a-71b9-81ae-2a6c45180fb9",
// "0195620e-18e1-7fbf-8c8f-d0578a3b1cc3",
// "0195620e-20a3-7ab7-8b5e-ab5e1dba703e",
// "0195620e-277c-7105-bc1a-7d607590ee5e",
// "0195620e-3897-711a-9741-fbcfbfdcdcb6",
// "0195620e-43ed-7db4-88c8-94517c4419b6",
// "0195620e-5995-7c73-a382-80ca2467627b",
// "0195620e-6d45-7d88-8a24-725d30cf8b74",
// "0195620e-76a4-7e47-8f7a-e6e9739d0c45",
