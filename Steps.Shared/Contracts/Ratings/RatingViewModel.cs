using Steps.Domain.Entities;

namespace Steps.Shared.Contracts.Ratings;

public class RatingViewModel
{
    public List<Rating> NotCompleted { get; set; } = [];
}