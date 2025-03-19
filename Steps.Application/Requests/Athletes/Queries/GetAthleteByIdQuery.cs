using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Requests.Clubs.Queries;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Athletes.Queries;

public record GetAthleteByIdQuery(Guid Id) : IRequest<Result<AthleteViewModel>>;

public class GetAthleteByIdQueryHandler : IRequestHandler<GetAthleteByIdQuery, Result<AthleteViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthleteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<AthleteViewModel>> Handle(GetAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Athlete>();
        var athlete = await repository.GetFirstOrDefaultAsync(
                          predicate: a => a.Id == request.Id,
                          trackingType: TrackingType.NoTracking)
                      ?? throw new AppNotFoundException("Участник не найден");

        var mapped = _mapper.Map<AthleteViewModel>(athlete);

        return Result<AthleteViewModel>.Ok(mapped);
    }
}