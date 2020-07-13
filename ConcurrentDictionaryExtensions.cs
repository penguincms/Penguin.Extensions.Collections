using System.Collections.Concurrent;
using System.Diagnostics.Contracts;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class ConcurrentDictionaryExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Adds a new item to the concurrent dictionary or updates an existing item with a matching key
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TValue">The type of the value</typeparam>
        /// <param name="dict">The source dictionary</param>
        /// <param name="key">The key to search for</param>
        /// <param name="value">The value to set the key to, or the new value to add</param>
        /// <returns>Whether or not the operation was successful</returns>
        public static bool TryAddOrUpdate<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict is null)
            {
                throw new System.ArgumentNullException(nameof(dict));
            }

            if (dict.ContainsKey(key))
            {
                dict[key] = value;
                return true;
            }
            else
            {
                return dict.TryAdd(key, value);
            }
        }
    }
}