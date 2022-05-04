using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class ListExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Removes and returns the first item from a list
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="list">The source list</param>
        /// <returns>The first item</returns>
        public static T Dequeue<T>(this IList<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            T item = list.ElementAt(0);
            list.RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack<T>(this T[] list, T item, bool isFirstToEnd)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            for (int index = 0; index < list.Length; index++)
            {
                if (list[index].Equals(item))
                {
                    list.MoveBack(index, isFirstToEnd);
                }
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="itemSelector">The predicate to use to find the item</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack<T>(this T[] list, Predicate<T> itemSelector, bool isFirstToEnd)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            int index = list.FindIndex(itemSelector);

            if (index == -1)
            {
                return;
            }

            list.MoveBack(index, isFirstToEnd);
        }

        /// <summary>
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="index">The index of the item to move</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack<T>(this T[] list, int index, bool isFirstToEnd)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            bool isFirst = index == 0;

            if (isFirstToEnd && isFirst)
            {
                (list[index], list[list.Length - 1]) = (list[list.Length - 1], list[index]);
            }
            else if (!isFirst)
            {
                (list[index], list[index - 1]) = (list[index - 1], list[index]);
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the end
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="item">The item to move</param>
        /// <param name="isLastToBeginning">A bool indicating whether or not the last item should move to the front</param>
        public static void MoveForward<T>(this T[] list, T item, bool isLastToBeginning)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            for (int index = 0; index < list.Length; index++)
            {
                if (list[index].Equals(item))
                {
                    list.MoveForward(index, isLastToBeginning);
                }
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the end
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="itemSelector">The predicate to use to find the item</param>
        /// <param name="isLastToBeginning">A bool indicating whether or not the last item should move to the front</param>
        public static void MoveForward<T>(this T[] list, Predicate<T> itemSelector, bool isLastToBeginning)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            int index = list.FindIndex(itemSelector);

            if (index == -1)
            {
                return;
            }

            list.MoveForward(index, isLastToBeginning);
        }

        /// <summary>
        /// Moves and item in a list one space towards the end
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="index">The index of the item to move</param>
        /// <param name="isLastToBeginning">A bool indicating whether or not the last item should move to the front</param>
        public static void MoveForward<T>(this T[] list, int index, bool isLastToBeginning)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            bool isLast = list.Length - 1 == index;

            if (isLastToBeginning && isLast)
            {
                (list[index], list[0]) = (list[0], list[index]);
            }
            else if (!isLast)
            {
                (list[index], list[index + 1]) = (list[index + 1], list[index]);
            }
        }

        public static int FindIndex(this IEnumerable source, Predicate<object> itemSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (itemSelector is null)
            {
                throw new ArgumentNullException(nameof(itemSelector));
            }

            int currentIndex = -1;
            int c = 0;
            foreach (object t in source)
            {
                if (itemSelector(t))
                {
                    currentIndex = c;
                    break;
                }
            }

            return currentIndex;
        }

        public static IEnumerable<int> FindIndexes<T>(this IEnumerable source, Predicate<object> itemSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (itemSelector is null)
            {
                throw new ArgumentNullException(nameof(itemSelector));
            }

            int c = 0;
            foreach (object t in source)
            {
                if (itemSelector(t))
                {
                    yield return c;
                }
            }
        }

        public static int FindIndex<T>(this IEnumerable<T> source, Predicate<T> itemSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (itemSelector is null)
            {
                throw new ArgumentNullException(nameof(itemSelector));
            }

            int currentIndex = -1;
            int c = 0;
            foreach (T t in source)
            {
                if (itemSelector(t))
                {
                    currentIndex = c;
                    break;
                }
            }

            return currentIndex;
        }

        /// <summary>
        /// Returns an IEnumerable of indexes for items matching the provided predicate
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="source">The source IEnumerable</param>
        /// <param name="itemSelector">The predicate for matching the items in the IEnumerable</param>
        /// <returns></returns>
        public static IEnumerable<int> FindIndexes<T>(this IEnumerable<T> source, Predicate<T> itemSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (itemSelector is null)
            {
                throw new ArgumentNullException(nameof(itemSelector));
            }

            int c = 0;
            foreach (T t in source)
            {
                if (itemSelector(t))
                {
                    yield return c;
                }
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="list">The target list</param>
        /// <param name="itemSelector">The predicate to use to find the item</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack<T>(this IList<T> list, Predicate<T> itemSelector, bool isFirstToEnd)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            int currentIndex = list.FindIndex(itemSelector);

            if (currentIndex == -1)
            {
                return;
            }

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
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            int currentIndex = list.FindIndex(itemSelector);

            if (currentIndex == -1)
            {
                return;
            }

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
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <param name="list">The target list</param>
        /// <param name="itemSelector">The predicate to use to find the item</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack(this IList list, Predicate<object> itemSelector, bool isFirstToEnd)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (itemSelector is null)
            {
                throw new ArgumentNullException(nameof(itemSelector));
            }

            int currentIndex = list.FindIndex(itemSelector);

            list.MoveBack(currentIndex, isFirstToEnd);
        }

        /// <summary>
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <param name="list">The target list</param>
        /// <param name="item">The item to move</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack(this IList list, object item, bool isFirstToEnd)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            int index = 0;
            foreach (object o in list)
            {
                if (item.Equals(o))
                {
                    list.MoveBack(index, isFirstToEnd);
                }

                index++;
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the front
        /// </summary>
        /// <param name="list">The target list</param>
        /// <param name="index">The index of the item to move</param>
        /// <param name="isFirstToEnd">A bool representing whether or not the first item should wrap to the end of the list</param>
        public static void MoveBack(this IList list, int index, bool isFirstToEnd)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            // Copy the current item
            object item = list[index];

            bool isFirst = index == 0;

            if (isFirstToEnd && isFirst)
            {
                // Remove the item
                list.RemoveAt(index);

                // add the item to the end
                _ = list.Add(item);
            }
            else if (!isFirst)
            {
                // Remove the item
                list.RemoveAt(index);

                // add the item to previous index
                list.Insert(index - 1, item);
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the end
        /// </summary>
        /// <param name="list">The target list</param>
        /// <param name="itemSelector">The predicate to use to find the item</param>
        /// <param name="isLastToBeginning">A bool indicating whether or not the last item should move to the front</param>
        public static void MoveForward(this IList list, Predicate<object> itemSelector, bool isLastToBeginning)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (itemSelector is null)
            {
                throw new ArgumentNullException(nameof(itemSelector));
            }

            int currentIndex = list.FindIndex(itemSelector);

            list.MoveForward(currentIndex, isLastToBeginning);
        }

        /// <summary>
        /// Moves and item in a list one space towards the end
        /// </summary>
        /// <param name="list">The target list</param>
        /// <param name="item">The item to move</param>
        /// <param name="isLastToBeginning">A bool indicating whether or not the last item should move to the front</param>
        public static void MoveForward(this IList list, object item, bool isLastToBeginning)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            int index = 0;
            foreach (object o in list)
            {
                if (item.Equals(o))
                {
                    list.MoveForward(index, isLastToBeginning);
                }

                index++;
            }
        }

        /// <summary>
        /// Moves and item in a list one space towards the end
        /// </summary>
        /// <param name="list">The target list</param>
        /// <param name="index">The index of the item to move</param>
        /// <param name="isLastToBeginning">A bool indicating whether or not the last item should move to the front</param>
        public static void MoveForward(this IList list, int index, bool isLastToBeginning)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            // Copy the current item
            object item = list[index];

            bool isLast = list.Count - 1 == index;

            if (isLastToBeginning && isLast)
            {
                // Remove the item
                list.RemoveAt(index);

                // add the item to the beginning
                list.Insert(0, item);
            }
            else if (!isLast)
            {
                // Remove the item
                list.RemoveAt(index);

                // add the item at next index
                list.Insert(index + 1, item);
            }
        }

        /// <summary>
        /// Recursively processes a function on a list
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="queue">The top most list on the tree</param>
        /// <param name="processingFunc">A function accepting a child and returning the next list of nodes to add to the processing queue</param>
        public static void RecursiveProcess<T>(this IEnumerable<T> queue, Func<T, IEnumerable<T>> processingFunc)
        {
            if (queue is null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (processingFunc is null)
            {
                throw new ArgumentNullException(nameof(processingFunc));
            }

            List<T> toProcess;

            if (queue is ICollection<T> collection)
            {
                toProcess = new List<T>(collection.Count);
            }
            else
            {
                toProcess = new List<T>();
            }

            foreach (T item in queue)
            {
                toProcess.Add(item);
            }

            IEnumerable<T> result;
            while (toProcess.Any())
            {
                T item = toProcess.Dequeue();

                result = processingFunc.Invoke(item);

                if (result != null)
                {
                    toProcess.AddRange(result);
                }
            }
        }

        private static System.Random InternalRandom { get; set; } = new Random();

        /// <summary>
        /// Chooses a random item from the source
        /// </summary>
        /// <typeparam name="T">Any item type</typeparam>
        /// <param name="source">The source collection to pick from</param>
        /// <returns>Any random item from the list</returns>
        public static T Random<T>(this ICollection<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ElementAt(InternalRandom.Next(0, source.Count));
        }
    }
}