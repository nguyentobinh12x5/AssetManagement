using System;
using System.Linq;
using System.Linq.Expressions;

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(
        this IQueryable<T> query,
        string sortColumnName,
        string sortDirection)
    {
        if (string.IsNullOrWhiteSpace(sortColumnName) ||
            string.IsNullOrWhiteSpace(sortDirection))
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, sortColumnName);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = sortDirection.Equals("Descending", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            query.Expression,
            Expression.Quote(lambda)
        );

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}