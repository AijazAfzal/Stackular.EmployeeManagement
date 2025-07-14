using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Enums;
using System.Linq.Expressions;
using System.Reflection;

namespace Stackular.EmployeeManagement.Application.Services.Common
{
    public class SortingService : ISortingService
    {
        public IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortBy, SortDirection sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name"; 
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
            {
                // Fallback to a default property if invalid sortBy
                property = typeof(T).GetProperty("Name"); // Change "Id" to your preferred default
                if (property == null)
                    throw new ArgumentException($"Property '{sortBy}' not found on type '{typeof(T).Name}'.");
            }

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName = sortOrder == SortDirection.Ascending ? "OrderBy" : "OrderByDescending";

            var resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { typeof(T), property.PropertyType },
                query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(resultExp);
        }
    }
}
