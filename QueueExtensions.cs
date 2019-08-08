using System;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class QueueExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        #region Methods

        /// <summary>
        /// An AddRange equivalent for a Queue
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="queue">The target Queue</param>
        /// <param name="toAdd">The items to add</param>
        public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> toAdd)
        {
            foreach (T item in toAdd)
            {
                queue.Enqueue(item);
            }
        }

        /// <summary>
        /// Recursively processes a function on a Queue
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="queue">The top most queue on the tree</param>
        /// <param name="processingFunc">A function accepting a child and returning the next list of nodes to add to the processing queue</param>
        public static void RecursiveProcess<T>(this Queue<T> queue, Func<T, IEnumerable<T>> processingFunc)
        {
            Queue<T> toProcess = new Queue<T>();

            foreach (T item in queue)
            {
                toProcess.Enqueue(item);
            }

            IEnumerable<T> result;
            while (toProcess.Any())
            {
                T item = toProcess.Dequeue();

                result = processingFunc.Invoke(item);

                if (result != null)
                {
                    foreach (T child in result)
                    {
                        toProcess.Enqueue(child);
                    }
                }
            }
        }

        #endregion Methods
    }
}