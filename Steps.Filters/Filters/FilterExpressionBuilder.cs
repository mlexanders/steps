using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace Steps.Filters.Filters;

public static class FilterBuilder<T> where T : class, new()
{
    public static Expression<Func<T, bool>> QueryToExpression(FilterGroup filterGroup)
    {
        // var filterGroup = JsonSerializer.Deserialize<FilterGroup>(queryString, 
        //     new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (filterGroup == null || filterGroup.Filters == null || !filterGroup.Filters.Any())
            throw new ArgumentException("Invalid or empty filter query");

        var param = Expression.Parameter(typeof(T), nameof(T));
        Expression? combinedExpression = null;

        foreach (var filter in filterGroup.Filters)
        {
            var propertyInfo = typeof(T).GetProperty(filter.Param);
            if (propertyInfo == null)
                throw new ArgumentException($"Property '{filter.Param}' not found in {nameof(T)}");

            var property = Expression.Property(param, propertyInfo);
            var value = ConvertValue(filter.Val, property.Type);
            var constant = Expression.Constant(value);

            Expression condition = filter.Op switch
            {
                "eq" => Expression.Equal(property, constant),
                "neq" => Expression.NotEqual(property, constant),
                "lt" => Expression.LessThan(property, constant),
                "lte" => Expression.LessThanOrEqual(property, constant),
                "gt" => Expression.GreaterThan(property, constant),
                "gte" => Expression.GreaterThanOrEqual(property, constant),
                _ => throw new NotSupportedException($"Unsupported operator: {filter.Op}")
            };

            if (combinedExpression == null)
                combinedExpression = condition;
            else
            {
                combinedExpression = filterGroup.Logic.ToLower() switch
                {
                    "or" => Expression.OrElse(combinedExpression, condition),
                    _ => Expression.AndAlso(combinedExpression, condition)
                };
            }
        }

        return Expression.Lambda<Func<T, bool>>(combinedExpression!, param);
    }
    
    private static object? ConvertValue(string value, Type targetType)
    {
        if (targetType == typeof(Guid))
        {
            return Guid.Parse(value); // Явное приведение строки в Guid
        }
    
        return Convert.ChangeType(value, targetType);
    }

    public static FilterGroup ExpressionToQueryString(Expression<Func<T, bool>> expression)
    {
        if (expression.Body is not BinaryExpression binaryExpr)
            throw new ArgumentException("Unsupported expression format");

        var filters = new List<FilterModel>();
        string logic = ExtractFilters(binaryExpr, filters);

        var filterGroup = new FilterGroup { Logic = logic, Filters = filters };
        return filterGroup;
    }

    private static string ExtractFilters(Expression expression, List<FilterModel> filters)
    {
        if (expression is BinaryExpression binaryExpr)
        {
            if (binaryExpr.NodeType == ExpressionType.AndAlso || binaryExpr.NodeType == ExpressionType.OrElse)
            {
                string logic = binaryExpr.NodeType == ExpressionType.AndAlso ? "and" : "or";
                ExtractFilters(binaryExpr.Left, filters);
                ExtractFilters(binaryExpr.Right, filters);
                return logic;
            }

            var filter = ConvertBinaryExpressionToFilter(binaryExpr);
            if (filter != null) filters.Add(filter);
        }
        return "and"; // По умолчанию
    }

    private static FilterModel? ConvertBinaryExpressionToFilter(BinaryExpression binaryExpr)
    {
        if (binaryExpr.Left is MemberExpression memberExpr)
        {
            object? value = null;

            if (binaryExpr.Right is ConstantExpression constantExpr)
            {
                value = constantExpr.Value;
            }
            else if (binaryExpr.Right is MemberExpression rightMember)
            {
                // Обрабатываем случаи, если значение - статическое поле или свойство
                if (rightMember.Member is FieldInfo fieldInfo)
                {
                    value = fieldInfo.GetValue(null); // Получаем значение статического поля
                }
                else if (rightMember.Member is PropertyInfo propertyInfo)
                {
                    value = propertyInfo.GetValue(null); // Получаем значение статического свойства
                }
            }
            else if (binaryExpr.Right is MethodCallExpression methodCallExpr &&
                     methodCallExpr.Method.Name == "get_Empty" &&
                     methodCallExpr.Method.DeclaringType == typeof(Guid))
            {
                // Обработка вызова Guid.Empty через get-метод
                value = Guid.Empty;
            }

            if (value != null)
            {
                return new FilterModel
                {
                    Param = memberExpr.Member.Name,
                    Op = GetOperator(binaryExpr.NodeType),
                    Val = value.ToString() ?? ""
                };
            }
        }
        return null;
    }

    private static string GetOperator(ExpressionType nodeType) => nodeType switch
    {
        ExpressionType.Equal => "eq",
        ExpressionType.NotEqual => "neq",
        ExpressionType.LessThan => "lt",
        ExpressionType.LessThanOrEqual => "lte",
        ExpressionType.GreaterThan => "gt",
        ExpressionType.GreaterThanOrEqual => "gte",
        _ => throw new NotSupportedException($"Unsupported expression type: {nodeType}")
    };
}