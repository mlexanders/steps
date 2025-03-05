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
        Specification = specification;
        var expressions = Specification?.GetExpressions();
        Predicate = expressions?.Predicate;
        Includes = expressions?.Includes?.Compile();
    }

    public SpecificationRequest<T> AddPredicate(Expression<Func<T, bool>> predicate)
    {
        Predicate = Combine(Predicate, predicate);
        return this;
    }
    
    private static Expression<TE> Combine<TE>(
        Expression<TE>? firstExpression,
        Expression<TE>? secondExpression)
    {
        if (firstExpression is null)
        {
            return secondExpression;
        }

        if (secondExpression is null)
        {
            return firstExpression;
        }

        var invokedExpression = Expression.Invoke(
            secondExpression,
            firstExpression.Parameters);

        var combinedExpression = Expression.AndAlso(firstExpression.Body, invokedExpression);

        return Expression.Lambda<TE>(combinedExpression, firstExpression.Parameters);
    }
}