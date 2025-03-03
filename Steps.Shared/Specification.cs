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

    public SpecificationContainer<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>> 
        GetExpresions()
    {
        var a = ExpressionSerializer.DeserializeText(Predicate);
        return new SpecificationContainer<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>>
        {
            Predicate = (Expression<Func<T, bool>>)ExpressionSerializer.DeserializeText(Predicate),
            Includes = Includes == null ? null : (Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>)ExpressionSerializer.DeserializeText(Includes)
        };
    }
    
    public class SpecificationContainer<TPredicate, TIncludes>
    {
        public TPredicate?  Predicate { get; set; }
        public TIncludes? Includes { get; set; }
    }
}

