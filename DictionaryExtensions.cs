﻿using System.Collections.Generic;
using System.Text;

namespace Penguin.Extensions.Collections
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class DictionaryExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Returns the dictionary in a formatted single string for later parsing, or rendering, or storage
        /// </summary>
        /// <typeparam name="TKey">Any type that can be represented as a string</typeparam>
        /// <typeparam name="TValue">Any type that can be represented as a string</typeparam>
        /// <param name="dict">The source dictionary</param>
        /// <param name="seperator">The character to insert between the key and value</param>
        /// <param name="terminator">The character to insert after each key value pair</param>
        /// <returns>A string representing the contents of the dictionary</returns>
        public static string ToFormattedString<TKey, TValue>(this Dictionary<TKey, TValue> dict, char seperator = '=', char terminator = ';')
        {
            if (dict is null)
            {
                throw new System.ArgumentNullException(nameof(dict));
            }

            StringBuilder sb = new();

            foreach (KeyValuePair<TKey, TValue> kvp in dict)
            {
                _ = sb.Append(kvp.Key);
                _ = sb.Append(seperator);
                _ = sb.Append(kvp.Value);
                _ = sb.Append(terminator);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Adds a value to the dictionary with the given key if it does not exist, or updates the given key if it does
        /// </summary>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="dict">The dictionary to alter</param>
        /// <param name="key">The key to add or update</param>
        /// <param name="value">The value to add or update</param>
        /// <returns>true if the value was added, false if it was updated</returns>
        public static bool AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict is null)
            {
                throw new System.ArgumentNullException(nameof(dict));
            }

            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
                return true;
            }
            else
            {
                dict[key] = value;
                return false;
            }
        }

        /// <summary>
        /// Adds a range of KeyValuePairs to the dictionary
        /// </summary>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <typeparam name="TValue">The value type</typeparam>
        /// <param name="dict">The target dictionary</param>
        /// <param name="range">An IEnumerable of the keys to add</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> range)
        {
            if (dict is null)
            {
                throw new System.ArgumentNullException(nameof(dict));
            }

            if (range is null)
            {
                throw new System.ArgumentNullException(nameof(range));
            }

            foreach (KeyValuePair<TKey, TValue> kvp in range)
            {
                dict.Add(kvp);
            }
        }
    }
}