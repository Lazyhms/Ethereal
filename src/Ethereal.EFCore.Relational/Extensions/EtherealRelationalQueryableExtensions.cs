// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// RelationalQueryableExtensions
    /// </summary>
    public static class EtherealRelationalQueryableExtensions
    {
        private static IReadOnlyCollection<StoreObjectType> StoreObjectTypes { get; }

        static EtherealRelationalQueryableExtensions()
            => StoreObjectTypes = Enum.GetValues<StoreObjectType>();

        /// <summary>
        /// Query the output variable of a given entity property.
        /// </summary>
        public static IQueryable<TEntity> FromConstant<TEntity>(this DbSet<TEntity> source, TEntity entity) where TEntity : class
        {
            var parameters = new List<object?>();

            var sqlBuilder = FromSqlBuilder(source, entity, parameters);
            return source.FromSqlRaw(sqlBuilder.ToString(), parameters.ToArray()!);
        }

        /// <summary>
        /// Query the output variable of a given entity property and contain with 'UNION'.
        /// </summary>
        public static IQueryable<TEntity> FromConstant<TEntity>(this DbSet<TEntity> source, params TEntity[] entities) where TEntity : class
            => source.FromConstant((IEnumerable<TEntity>)entities);

        /// <summary>
        /// Query the output variable of a given entity property and contain with 'UNION'.
        /// </summary>
        public static IQueryable<TEntity> FromConstant<TEntity>(this DbSet<TEntity> source, IEnumerable<TEntity> entities) where TEntity : class
        {
            var parameters = new List<object?>();
            var fromSql = FromSqlBuilder(source, entities, parameters, false);
            return source.FromSqlRaw(fromSql, parameters.ToArray()!);
        }

        /// <summary>
        /// Query the output variable of a given entity property and contain with 'UNION ALL'.
        /// </summary>
        public static IQueryable<TEntity> FromConstantAll<TEntity>(this DbSet<TEntity> source, params TEntity[] entities) where TEntity : class
            => source.FromConstantAll((IEnumerable<TEntity>)entities);

        /// <summary>
        /// Query the output variable of a given entity property and contain with 'UNION ALL'.
        /// </summary>
        public static IQueryable<TEntity> FromConstantAll<TEntity>(this DbSet<TEntity> source, IEnumerable<TEntity> entities) where TEntity : class
        {
            var parameters = new List<object?>();
            var fromSql = FromSqlBuilder(source, entities, parameters, true);
            return source.FromSqlRaw(fromSql, parameters.ToArray()!);
        }

        private static string FromSqlBuilder<TEntity>(DbSet<TEntity> source, IEnumerable<TEntity> entities, List<object?> parameters, bool unionAll) where TEntity : class
        {
            var stringBuilder = new List<StringBuilder>();
            foreach (var entity in entities)
            {
                stringBuilder.Add(FromSqlBuilder(source, entity, parameters));
            }
            return string.Join($"\r\nUNION{(unionAll ? " ALL" : string.Empty)}\r\n", stringBuilder);
        }

        private static StringBuilder FromSqlBuilder<TEntity>(DbSet<TEntity> source, TEntity entity, List<object?> parameters) where TEntity : class
        {
            var sqlBuilder = new StringBuilder("SELECT ");
            var storeObject = StoreObjectTypes.Select(storeObjectType => StoreObjectIdentifier.Create(source.EntityType, storeObjectType))
                .FirstOrDefault(storeObjectIdentifier => storeObjectIdentifier is not null);

            foreach (var property in source.EntityType.GetProperties())
            {
                parameters.Add(typeof(TEntity).GetProperty(property.Name)?.GetValue(entity) ?? property.GetDefaultValue());
                sqlBuilder.Append($"@p{parameters.Count - 1} AS [{property.GetColumnName(storeObject.GetValueOrDefault())}] ,");
            }

            sqlBuilder.Length -= ",".Length;
            return sqlBuilder;
        }
    }
}
