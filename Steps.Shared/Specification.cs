using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Serialize.Linq.Serializers;

namespace Steps.Shared;

public class Specification<T> where T : class
{
    public string Predicate { get; set; } = null!;
    public string? Includes { get; set; }
    
    private ExpressionSerializer? _expressionSerializer;

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

