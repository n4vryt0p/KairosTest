using System;
using System.Linq;
using System.Linq.Expressions;

namespace KairosTest.Handlers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortColumn, string sortColumnDir)
        {
            var expression = source.Expression;
            sortColumn = sortColumn.First().ToString().ToUpper() + sortColumn.Substring(1);
            var parameter = Expression.Parameter(typeof(T), "x");
            var selector = Expression.PropertyOrField(parameter, sortColumn);
            var method = string.Equals(sortColumnDir, "desc", StringComparison.OrdinalIgnoreCase) ?
                "OrderByDescending" : "OrderBy";
            expression = Expression.Call(typeof(Queryable), method,
                new[] { source.ElementType, selector.Type },
                expression, Expression.Quote(Expression.Lambda(selector, parameter)));
            return source.Provider.CreateQuery<T>(expression);
        }
    }
}
