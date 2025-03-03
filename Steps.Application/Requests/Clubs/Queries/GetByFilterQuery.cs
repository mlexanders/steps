using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Filters.Filters;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Application.Requests.Clubs.Queries;

public record GetByFilterQuery(FilterGroup Filter) : IRequest<Result<List<ClubViewModel>>>;

public class GetByFilterQueryHandler : IRequestHandler<GetByFilterQuery, Result<List<ClubViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByFilterQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<ClubViewModel>>> Handle(GetByFilterQuery request, CancellationToken cancellationToken)
    {
        var expression = FilterBuilder<Club>.QueryToExpression(request.Filter);

        var repository = _unitOfWork.GetRepository<Club>();
        if (expression is null) throw new NullReferenceException(nameof(request.Filter));

        var clubs = await repository.GetAllAsync(
            predicate: expression,
            selector: c => _mapper.Map<ClubViewModel>(c),
            trackingType: TrackingType.NoTracking);

        return Result<List<ClubViewModel>>.Ok(clubs.ToList());
    }
}