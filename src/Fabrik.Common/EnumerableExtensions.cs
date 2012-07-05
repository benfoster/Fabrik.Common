using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Fabrik.Common
{
    /// <summary>
    /// Extension methods for <see cref="System.Collections.Generic.IEnumerable{T}"/>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs an action on each value of the enumerable
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="enumerable">Sequence on which to perform action</param>
        /// <param name="action">Action to perform on every item</param>
        /// <exception cref="System.ArgumentNullException">Thrown when given null <paramref name="enumerable"/> or <paramref name="action"/></exception>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Ensure.Argument.NotNull(enumerable, "enumerable");
            Ensure.Argument.NotNull(action, "action");

            foreach (T value in enumerable)
            {
                action(value);
            }
        }

        /// <summary>
        /// Convenience method for retrieving a specific page of items within a collection.
        /// </summary>
        /// <param name="pageSize">The size of the pages.</param>
        /// <param name="pageIndex">The index of the page to get.</param>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageSize, int pageIndex)
        {
            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Converts an enumerable into a readonly collection
        /// </summary>
        public static IEnumerable<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ReadOnlyCollection<T>(enumerable.ToList());
        }
    }
}
