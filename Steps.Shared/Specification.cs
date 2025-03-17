using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Serialize.Linq.Serializers;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;

namespace Steps.Shared;

public class Specification<T> where T : class
{
    public string Predicate { get; set; } = null!;
    public string? Includes { get; set; }
    
    private ExpressionSerializer? _expressionSerializer;

    public Specification()
    {
        
    }
    
    public Specification(params Type[] types)
    {
        ExpressionSerializer.AddKnownTypes(types);
    }

    private ExpressionSerializer ExpressionSerializer =>
        _expressionSerializer ??= new ExpressionSerializer(new JsonSerializer());

    public Specification<T> Where(Expression<Func<T, bool>> predicate)
    {
        Predicate = ExpressionSerializer.SerializeText(predicate);
        return this;
    }

    public Specification<T> Include(Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>?> includes)
    {
        Includes = ExpressionSerializer.SerializeText(includes);
        return this;
    }

    public Specification<T> AddPredicate(Expression<Func<T, bool>> combinePredicate)
    {
        if (string.IsNullOrEmpty(Predicate))
        {
            Where(combinePredicate);
        }
        var predicate = GetExpressions().Predicate;
        var combined = Combine(predicate, combinePredicate);
        Where(combined);
        
        return this;
    }
    
    public Specification<T> AddInclude(Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>?> include)
    {
        if (Includes is null)
        {
            Include(include);
        }
        var includes = GetExpressions().Includes;
        var combined = Combine(includes, include);
        Include(combined);
        
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

    public SpecificationContainer<Expression<Func<T, bool>>?, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>?>>
    GetExpressions()
    {
        Expression<Func<T, bool>>? predicate = null;
        Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>?>? includes = null;

        if (!string.IsNullOrEmpty(Predicate))
        {
            var predicateObj = ExpressionSerializer.DeserializeText(Predicate);
            if (predicateObj is Expression<Func<T, bool>> predicateExpr)
            {
                predicate = predicateExpr;
            }
        }

        if (!string.IsNullOrEmpty(Includes))
        {
            var includesObj = ExpressionSerializer.DeserializeText(Includes);
            if (includesObj is Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> includesExpr)
            {
                includes = includesExpr;
            }
        }

        return new SpecificationContainer<Expression<Func<T, bool>>?, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>?>>
        {
            Predicate = predicate,
            Includes = includes
        };
    }

    public class SpecificationContainer<TPredicate, TIncludes>
    {
        public TPredicate?  Predicate { get; set; }
        public TIncludes? Includes { get; set; }
    }
}

public static class Specification
{
    public static Specification<User> Users => new Specification<User>(typeof(Role));
    public static Specification<Athlete> Athletes => new Specification<Athlete>(typeof(AthleteType), typeof(Degree));

    public static Specification<Contest> Contests => new Specification<Contest>(typeof(ContestStatus), typeof(ContestType));
}