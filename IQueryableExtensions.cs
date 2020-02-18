using System.Collections.Generic;
using System.Linq;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class IQueryableExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Converts an IQueryable to a typed list
        /// </summary>
        /// <typeparam name="T">The type filter to apply</typeparam>
        /// <param name="source">The source IQueryable</param>
        /// <returns>A Typed List</returns>
        public static IList<T> ToTypedList<T>(this IQueryable source)
        {
            return Enumerable.ToList(source.OfType<T>());
        }

        /// <summary>
        /// Converts an IQueryable to a typed list
        /// </summary>
        /// <param name="source">The source IQueryable</param>
        /// <returns>A Typed List</returns>
        public static IEnumerable<object> AsIEnumerable(this IQueryable source)
        {
            foreach (object o in source)
            {
                yield return o;
            }
        }
    }
}