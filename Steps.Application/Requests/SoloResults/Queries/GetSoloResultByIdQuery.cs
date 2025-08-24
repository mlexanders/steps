using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Application.Requests.SoloResults.Queries;

public record GetSoloResultByIdQuery(Guid Id) : IRequest<Result<SoloResultViewModel>>;

public class GetSoloResultByIdQueryHandler : IRequestHandler<GetSoloResultByIdQuery, Result<SoloResultViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSoloResultByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<SoloResultViewModel>> Handle(GetSoloResultByIdQuery request,
        CancellationToken cancellationToken)
    {
        var soloResult = await _unitOfWork.GetRepository<SoloResult>()
            .GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Id,
                include: x => x.Include(s => s.Athlete),
                trackingType: TrackingType.NoTracking,
                cancellationToken: cancellationToken);

        if (soloResult == null)
            return Result<SoloResultViewModel>.Fail("Результат не найден");

        var viewModel = _mapper.Map<SoloResultViewModel>(soloResult);
        return Result<SoloResultViewModel>.Ok(viewModel);
    }
}
