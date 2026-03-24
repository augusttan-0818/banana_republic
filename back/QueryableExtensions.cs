using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using System.Linq.Expressions;
using System.Reflection;

namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Querying
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyFilters<T>(
            this IQueryable<T> query,
            List<FilterRule> filters)
        {
            foreach (var filter in filters)
            {
                query = query.Where(ExpressionBuilder.BuildExpression<T>(filter));
            }
            return query;
        }

        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            List<SortRule> sortRules)
        {
            bool first = true;

            foreach (var sort in sortRules)
            {
                query = first
                    ? query.OrderByDynamic(sort.Field, sort.Direction)
                    : ((IOrderedQueryable<T>)query)
                        .ThenByDynamic(sort.Field, sort.Direction);

                first = false;
            }

            return query;
        }

        public static IQueryable<T> OrderByDynamic<T>(
           this IQueryable<T> query,
           string field,
           string direction)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var property = typeof(T).GetProperty(field,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                ?? throw new ArgumentException($"Invalid sort field: {field}");

            var propertyAccess = Expression.Property(parameter, property);

            var orderByExpression =
                Expression.Lambda(propertyAccess, parameter);

            var methodName = direction.ToLower() == "desc"
                ? "OrderByDescending"
                : "OrderBy";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), property.PropertyType },
                query.Expression,
                Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        public static IQueryable<T> ThenByDynamic<T>(
    this IOrderedQueryable<T> query,
    string field,
    string direction)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var property = typeof(T).GetProperty(field,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                ?? throw new ArgumentException($"Invalid sort field: {field}");

            var propertyAccess = Expression.Property(parameter, property);

            var orderByExpression =
                Expression.Lambda(propertyAccess, parameter);

            var methodName = direction.ToLower() == "desc"
                ? "ThenByDescending"
                : "ThenBy";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), property.PropertyType },
                query.Expression,
                Expression.Quote(orderByExpression));

            return (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}


