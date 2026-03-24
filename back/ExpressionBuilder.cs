using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using System.Linq.Expressions;

namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Querying
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> BuildExpression<T>(FilterRule filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filter.Field);
            var constant = Expression.Constant(Convert.ChangeType(filter.Value, property.Type));

            Expression body = filter.Operator switch
            {
                "eq" => Expression.Equal(property, constant),
                "contains" => Expression.Call(
                    property,
                    "Contains",
                    null,
                    constant),
                _ => throw new NotSupportedException()
            };

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
