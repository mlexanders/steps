using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.AthletesElements.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.AthleteElements.Queries;

public record GetAthleteElementsQuery(string Degree, string AgeCategory, string? Type)
    : IRequest<Result<AthleteElementsViewModel>>;
    
public class GetAthleteElementsQueryHandler : IRequestHandler<GetAthleteElementsQuery, Result<AthleteElementsViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthleteElementsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<AthleteElementsViewModel>> Handle(GetAthleteElementsQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Domain.Entities.AthleteElements>();

        var athleteElements = new Domain.Entities.AthleteElements();

        if (request.AgeCategory == "Baby")
        {
            athleteElements = await repository.GetFirstOrDefaultAsync(
                predicate: x => x.Degree == request.Degree && x.AgeCategory == request.AgeCategory,
                trackingType: TrackingType.Tracking);
        }
        else
        {
            athleteElements = await repository.GetFirstOrDefaultAsync(
                predicate: x => x.Degree == request.Degree && x.AgeCategory == request.AgeCategory && x.Type == request.Type,
                trackingType: TrackingType.Tracking);
        }

        var mapped = _mapper.Map<AthleteElementsViewModel>(athleteElements);

        return Result<AthleteElementsViewModel>.Ok(mapped);
    }
}