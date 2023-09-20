using CrwnClothing.DAL.Models;
using CrwnClothing.DAL.Models.Sorting;
using System.Linq.Expressions;
using System.Reflection;

namespace CrwnClothing.DAL.Helpers
{
    public static class RepositoryHelper
    {
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, Pagination pagination)
        {

            return source.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize);
        }

        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, Pagination pagination)
        {

            return source.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize);
        }

        //Used for filtering TSource with optional filter property
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);


            return source;
        }

        public static IOrderedQueryable<TSource> OrderByPropertyName<TSource>(this IQueryable<TSource> source, string? orderByProperty,
                          OrderDirection? orderDirection)
        {
            string command =
                orderDirection == OrderDirection.Descending || orderDirection == null
                ? "OrderByDescending"
                : "OrderBy";

            var oderBy =
                orderByProperty == null || orderByProperty == String.Empty
                ? "Id"
                : orderByProperty;

            var type = typeof(TSource);
            var property = type.GetProperty(oderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                throw new Exception($"Unknow property ${orderByProperty}");

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));



            return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(resultExpression);
        }

    }
}
