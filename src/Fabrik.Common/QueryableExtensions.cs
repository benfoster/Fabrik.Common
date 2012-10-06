using System.Linq;

namespace Fabrik.Common
{
    /// <summary>
    /// Extension methods for <see cref="System.Linq.IQueryable{T}"/>
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Convenience method for performing paged queries.
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="query">Query to page</param>
        /// <param name="pageIndex">The index of the page to get.</param>
        /// <param name="pageSize">The size of the pages.</param>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            Ensure.Argument.NotNull(query, "query");
            Ensure.Argument.Is(pageIndex >= 0, "The page index cannot be negative.");
            Ensure.Argument.Is(pageSize > 0, "The page size must be greater than zero.");

            return query.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}
