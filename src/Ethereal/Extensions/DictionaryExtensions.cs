// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Dictionary
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Get value or add
        /// </summary>
        public static TValue GetOrAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            TKey key,
            TValue value) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));

            if (!source.TryGetValue(key, out _))
            {
                source.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// Get value or default
        /// </summary>
        public static TValue? GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            TKey key) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));

            return source.TryGetValue(key, out var value) ? value : default;
        }

        /// <summary>
        /// Try add or update item
        /// </summary>
        public static IDictionary<TKey, TValue> TryAddOrUpdate<TKey, TValue>(
           this IDictionary<TKey, TValue> source,
           TKey key,
           TValue value) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(key, nameof(key));

            if (!source.TryAdd(key, value))
            {
                return source.TryUpdate(key, value);
            }
            return source;
        }

        /// <summary>
        /// Try add or update item
        /// </summary>
        public static IDictionary<TKey, TValue> TryAddOrUpdate<TKey, TValue>(
           this IDictionary<TKey, TValue> source,
           KeyValuePair<TKey, TValue> item) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(item, nameof(item));

            if (!source.TryAdd(item.Key, item.Value))
            {
                return source.TryUpdate(item);
            }
            return source;
        }

        /// <summary>
        /// Try update item
        /// </summary>
        public static IDictionary<TKey, TValue> TryUpdate<TKey, TValue>(
           this IDictionary<TKey, TValue> source,
           TKey key,
           TValue value) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(key, nameof(key));

            if (source.ContainsKey(key))
            {
                source[key] = value;
            }
            return source;
        }

        /// <summary>
        /// Try update item
        /// </summary>
        public static IDictionary<TKey, TValue> TryUpdate<TKey, TValue>(
           this IDictionary<TKey, TValue> source,
           KeyValuePair<TKey, TValue> item) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(item, nameof(item));

            if (source.ContainsKey(item.Key))
            {
                source[item.Key] = item.Value;
            }
            return source;
        }

        /// <summary>
        /// Union
        /// </summary>
        public static IDictionary<TKey, TValue> Union<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            IDictionary<TKey, TValue> target) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(target, nameof(target));

            foreach (var item in target)
            {
                source.TryAddOrUpdate(item);
            }
            return source;
        }

        /// <summary>
        /// Try remove item
        /// </summary>
        public static bool TryRemove<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            TKey key,
            out TValue? value) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));

            if (source.TryGetValue(key, out value))
            {
                return source.Remove(key);
            }
            return false;
        }
    }
}