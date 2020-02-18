using Penguin.Reflection.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class IEnumerableExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Checks if an IEnumerable is Not null and contains objects
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="target">The IEnumerable to check</param>
        /// <returns>True if the IEnumerable is not null and contains any obejcts</returns>
        public static bool AnyNotNull<T>(this IEnumerable<T> target)
        {
            return target != null && target.Any();
        }

        /// <summary>
        /// Converts a list of objects to a list of strings and then calls Join with the specified delimeter
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="target">The IEnumerable to join</param>
        /// <param name="delimeter">The string delimeter to join with</param>
        /// <returns>True if the IEnumerable is not null and contains any obejcts</returns>
        public static string Join<T>(this IEnumerable<T> target, string delimeter = ", ")
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(delimeter));
            }

            return string.Join(delimeter, target.Select(o => $"{o}"));
        }

        /// <summary>
        /// Checks if an IEnumerable is Not null and contains objects
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="target">The IEnumerable to check</param>
        /// <param name="predicate">The predicate to check for</param>
        /// <returns>True if the IEnumerable is not null and contains any obejcts matching the predicate</returns>
        public static bool AnyNotNull<T>(this IEnumerable<T> target, Func<T, bool> predicate)
        {
            return target != null && target.Any(predicate);
        }

        /// <summary>
        /// Runs an action once for every object in the IEnumerable
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="oldQuery">The IEnumerable to check</param>
        /// <param name="toRun">The Action to run</param>
        public static void ForEach<T>(this IEnumerable<T> oldQuery, Action<T> toRun)
        {
            Contract.Requires(oldQuery != null);
            Contract.Requires(toRun != null);

            foreach (T item in oldQuery)
            {
                toRun.Invoke(item);
            }
        }

        /// <summary>
        /// Returns the shared base type of all objects in the list
        /// </summary>
        /// <typeparam name="T">The list type</typeparam>
        /// <param name="target">The list to check</param>
        /// <returns>A type that should be assignable from every object in the list</returns>
        public static Type GetCommonType<T>(this IEnumerable<T> target)
        {
            if (target is null)
            {
                return typeof(T);
            }

            //Start the type recursion based on the first list object. This will most likely be the return type
            //Since the list will most likely contain a single type
            Type commonType = target.First().GetType();

            //Now we look for a base Type to edit
            foreach (object thisEntity in target)
            {
                //as long as we dont inherit from the type of the first object
                while (!commonType.IsAssignableFrom(thisEntity.GetType()))
                {
                    //Step Up
                    commonType = commonType.BaseType;
                }

                //Eventually we may hit an object type. This is unhandled.
            }

            return commonType;
        }

        /// <summary>
        /// Combines ToList and Where into a single function
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="oldQuery">The IEnumerable target</param>
        /// <param name="filter">The predicate to pass to Where</param>
        /// <returns>The filtered list</returns>
        public static List<T> ToList<T>(this IEnumerable<T> oldQuery, Func<T, bool> filter) => oldQuery.Where(filter).ToList();

        /// <summary>
        /// Converts an IEnumerable to a Queue
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="list">The target list</param>
        /// <returns>A Queue in the order of the list</returns>
        public static Queue<T> ToQueue<T>(this IEnumerable<T> list)
        {
            Contract.Requires(list != null);

            Queue<T> queue = new Queue<T>();

            foreach (T item in list)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        /// <summary>
        /// Converts a non-generic IEnumerable to a typed list
        /// </summary>
        /// <param name="source">The non-generic IEnumerable source</param>
        /// <returns>A typed list of the IEnumerable source type</returns>
        public static IList ToGenericList(this IEnumerable source)
        {
            if (source is null)
            {
                return null;
            }

            Type collectionType = source.GetType().GetCollectionType();

            if (collectionType is null)
            {
                collectionType = typeof(object);
            }

            IList toReturn = Activator.CreateInstance(typeof(List<>).MakeGenericType(collectionType)) as IList;

            foreach (object o in source)
            {
                toReturn.Add(o);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns an IEnumerable from the source list containing only the specified type
        /// </summary>
        /// <typeparam name="T">The source list Type</typeparam>
        /// <param name="source">The source list</param>
        /// <param name="t">The type of objects to return from the source</param>
        /// <returns>An IEnumerable from the source list containing only the specified type</returns>
        public static IEnumerable<T> OfType<T>(this IEnumerable<T> source, Type t)
        {
            return source.Where(p => p.GetType() == t);
        }

        /// <summary>
        /// Returns an IEnumerable from the source list containing everything but the specified type
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="source">The source list</param>
        /// <returns>An IEnumerable from the source list containing everything but the specified type</returns>
        public static IEnumerable NotOfType<T>(this IEnumerable source) where T : class
        {
            Contract.Requires(source != null);

            foreach (object o in source)
            {
                if (!(o is T))
                {
                    yield return o;
                }
            }
        }

        /// <summary>
        /// Returns an IEnumerable from the source list containing everything but the specified type
        /// </summary>
        /// <typeparam name="TExclude">Any type that inherits from the source type</typeparam>
        /// <typeparam name="TSource">The source list Type</typeparam>
        /// <param name="source">The source list</param>
        /// <returns>An IEnumerable from the source list containing everything but the specified type</returns>
        public static IEnumerable<TSource> NotOfType<TExclude, TSource>(this IEnumerable<TSource> source) where TExclude : TSource
        {
            Contract.Requires(source != null);

            return source.Where(p => !(p is TExclude));
        }

        /// <summary>
        /// Returns an IEnumerable from the source list containing everything but the specified type
        /// </summary>
        /// <typeparam name="T">The source list Type</typeparam>
        /// <param name="source">The source list</param>
        /// <param name="t">The type of objects to return from the source</param>
        /// <returns>An IEnumerable from the source list containing everything but the specified type</returns>
        public static IEnumerable<T> NotOfType<T>(this IEnumerable<T> source, Type t)
        {
            return source.Where(p => p.GetType() != t);
        }
    }
}