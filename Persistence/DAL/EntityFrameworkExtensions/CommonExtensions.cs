using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.DAL.EntityFrameworkExtensions
{
    public static class CommonExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }
            return query;
        }
    }
}