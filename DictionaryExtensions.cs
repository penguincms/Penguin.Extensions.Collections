using System;
using System.Collections.Generic;
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
            StringBuilder sb = new StringBuilder();

            foreach(KeyValuePair<TKey, TValue> kvp in dict)
            {
                sb.Append(kvp.Key);
                sb.Append(seperator);
                sb.Append(kvp.Value);
                sb.Append(terminator);
            }

            return sb.ToString();
        }
    }
}
