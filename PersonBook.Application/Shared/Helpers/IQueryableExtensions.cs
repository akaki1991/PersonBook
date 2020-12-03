using System;
using System.Linq;
using System.Linq.Expressions;

namespace PersonBook.Application.Shared.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> WhereIfHas<TSource>(this IQueryable<TSource> source,
                                                              object value,
                                                              Expression<Func<TSource, bool>> predicate) where TSource : class
        {
            if (value != null)
            {
                if (value is string)
                {
                    if (string.IsNullOrEmpty((string)value)) return source;
                    value = ((string)value).Trim().ToLower();
                }

                return source.Where(predicate);
            }
            return source;
        }
    }
}
