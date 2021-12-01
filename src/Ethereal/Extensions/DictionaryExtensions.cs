// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// KeyValue
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// TryAddOrUpdate
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
                return source.Update(key, value);
            }
            return source;
        }

        /// <summary>
        /// TryAddOrUpdate
        /// </summary>
        public static IDictionary<TKey, TValue> TryAddOrUpdate<TKey, TValue>(
           this IDictionary<TKey, TValue> source,
           KeyValuePair<TKey, TValue> item) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(item, nameof(item));

            if (!source.TryAdd(item.Key, item.Value))
            {
                return source.Update(item);
            }
            return source;
        }

        /// <summary>
        /// TryUpdate
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
                return source.Update(key, value);
            }
            return source;
        }

        /// <summary>
        /// TryUpdate
        /// </summary>
        public static IDictionary<TKey, TValue> TryUpdate<TKey, TValue>(
           this IDictionary<TKey, TValue> source,
           KeyValuePair<TKey, TValue> item) where TKey : notnull
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(item, nameof(item));

            if (source.ContainsKey(item.Key))
            {
                return source.Update(item);
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
        /// Update
        /// </summary>
        public static IDictionary<TKey, TValue> Update<TKey, TValue>(
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
        /// Update
        /// </summary>
        public static IDictionary<TKey, TValue> Update<TKey, TValue>(
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
    }
}