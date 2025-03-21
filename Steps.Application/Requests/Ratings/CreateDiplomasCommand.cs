using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Ratings;

namespace Steps.Application.Requests.Ratings;

public record CreateDiplomasCommand(List<Rating> Ratings) : IRequest<Result<DiplomasViewModel>>;


public class CreateDiplomasCommandHandler : IRequestHandler<CreateDiplomasCommand, Result<DiplomasViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiplomasCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DiplomasViewModel>> Handle(CreateDiplomasCommand request, CancellationToken cancellationToken)
    {
        var ratingsIds = request.Ratings.Select(r => r.Id);

        var ratings = await _unitOfWork.GetRepository<Rating>().GetAllAsync(
            predicate: r => ratingsIds.Contains(r.Id)
            && !r.IsComplete,
            trackingType: TrackingType.Tracking);

        foreach (var rating in ratings)
        {
            rating.IsComplete = true;
        }

        await _unitOfWork.SaveChangesAsync();

        var view = new DiplomasViewModel()
        {
            Url = ""
        };
        
        return Result<DiplomasViewModel>.Ok(view);
    }
}
