using System;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ListExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        #region Methods

        /// <summary>
        /// Removes and returns the first item from a list
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="list">The source list</param>
        /// <returns>The first item</returns>
        public static T Dequeue<T>(this List<T> list)
        {
            T item = list.ElementAt(0);
            list.RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="itemSelector">The predicate to use to find the item</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack<T>(this List<T> list, Predicate<T> itemSelector, bool isFirstToEnd)
        {
            int currentIndex = list.FindIndex(itemSelector);

            // Copy the current item
            T item = list[currentIndex];

            bool isFirst = currentIndex == 0;

            if (isFirstToEnd && isFirst)
            {
                // Remove the item
                list.RemoveAt(currentIndex);

                // add the item to the end
                list.Add(item);
            }
            else if (!isFirst)
            {
                // Remove the item
                list.RemoveAt(currentIndex);

                // add the item to previous index
                list.Insert(currentIndex - 1, item);
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the end
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="itemSelector">The predicate to use to find the item</param>
        /// <param name="isLastToBeginning">A bool indicating whether or not the last item should move to the front</param>
        public static void MoveForward<T>(this List<T> list, Predicate<T> itemSelector, bool isLastToBeginning)
        {
            int currentIndex = list.FindIndex(itemSelector);

            // Copy the current item
            T item = list[currentIndex];

            bool isLast = list.Count - 1 == currentIndex;

            if (isLastToBeginning && isLast)
            {
                // Remove the item
                list.RemoveAt(currentIndex);

                // add the item to the beginning
                list.Insert(0, item);
            }
            else if (!isLast)
            {
                // Remove the item
                list.RemoveAt(currentIndex);

                // add the item at next index
                list.Insert(currentIndex + 1, item);
            }
        }

        /// <summary>
        /// Recursively processes a function on a list
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="queue">The top most list on the tree</param>
        /// <param name="processingFunc">A function accepting a child and returning the next list of nodes to add to the processing queue</param>
        public static void RecursiveProcess<T>(this List<T> queue, Func<T, IEnumerable<T>> processingFunc)
        {
            List<T> toProcess = new List<T>();

            foreach (T item in queue)
            {
                toProcess.Add(item);
            }

            IEnumerable<T> result;
            while (toProcess.Any())
            {
                T item = toProcess.Dequeue();

                if (item != null)
                {
                    result = processingFunc.Invoke(item);

                    if (result != null)
                    {
                        foreach (T child in result)
                        {
                            toProcess.Add(child);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Splits a list into evenly sized chunks
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="input">The source list</param>
        /// <param name="size">The size of the chunked lists</param>
        /// <returns>An IEnumerable used to access the collection of new child lists</returns>
        public static IEnumerable<List<T>> Split<T>(this List<T> input, int size)
        {
            for (int i = 0; i < input.Count; i += size)
            {
                yield return input.GetRange(i, Math.Min(size, input.Count - i));
            }
        }

        private static System.Random random { get; set; } = new Random();

        /// <summary>
        /// Chooses a random item from the source
        /// </summary>
        /// <typeparam name="T">Any item type</typeparam>
        /// <param name="source">The source collection to pick from</param>
        /// <returns>Any random item from the list</returns>
        public static T Random<T>(this ICollection<T> source)
        {
            return source.ElementAt(random.Next(0, source.Count));
        }

        #endregion Methods
    }
}