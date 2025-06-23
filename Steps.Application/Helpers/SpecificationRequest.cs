using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Steps.Shared;

namespace Steps.Application.Helpers;

public record SpecificationRequest<T> where T : class
{
    public Specification<T>? Specification { get; set; }
    
    public Expression<Func<T, bool>>? Predicate { get; set; }
    public Func<IQueryable<T>, IIncludableQueryable<T, object>>? Includes { get; set; }

    protected SpecificationRequest(Specification<T>? specification)
    {
        Init(specification);
    }

    private void Init(Specification<T>? specification)
    {
        Specification = specification;
        var expressions = Specification?.GetExpressions();
        Predicate = expressions?.Predicate;
        Includes = expressions?.Includes?.Compile();
    }

    public SpecificationRequest<T> AddPredicate(Expression<Func<T, bool>> predicate)
    {
        if (Specification is null)
        {
            Specification = new Specification<T>().Where(predicate);
        }
        else
        {
            Specification.AddPredicate(predicate);
        }
        
        Init(Specification);
        return this;
    }
}