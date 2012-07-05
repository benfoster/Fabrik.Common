using System.Collections.Generic;

namespace Fabrik.Common
{
    /// <summary>
    /// Extension methods for <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/>
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified key or the <paramref name="defaultValue"/> if it does not exist.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultValue">The default value to return if an item with the specified <paramref name="key"/> does not exist.</param>
        /// <returns>The value associated with the specified key or the <paramref name="defaultValue"/> if it does not exist.</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, 
            TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            
            if (dictionary.TryGetValue(key, out value))
                return value;

            return defaultValue;
        }
    }
}
