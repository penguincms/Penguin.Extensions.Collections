﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class QueueExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// An AddRange equivalent for a Queue
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="queue">The target Queue</param>
        /// <param name="toAdd">The items to add</param>
        public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> toAdd)
        {
            Contract.Requires(queue != null);
            Contract.Requires(toAdd != null);

            foreach (T item in toAdd)
            {
                queue.Enqueue(item);
            }
        }
    }
}