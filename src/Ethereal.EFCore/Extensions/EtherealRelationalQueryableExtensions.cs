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
        /// FromSqlInterpolated
        /// </summary>
        public static IQueryable<TEntity> FromConstant<TEntity>(this DbSet<TEntity> source, TEntity entity) where TEntity : class
        {
            var parameters = new List<object?>();
            var sqlBuilder = new StringBuilder(" SELECT ");

            foreach (var property in source.EntityType.GetProperties())
            {
                var storeObject = StoreObjectTypes.Select(storeObjectType => StoreObjectIdentifier.Create(property.DeclaringEntityType, storeObjectType))
                                    .FirstOrDefault(storeObjectIdentifier => storeObjectIdentifier is not null);

                parameters.Add(typeof(TEntity).GetProperty(property.Name)?.GetValue(entity) ?? property.GetDefaultValue());

                sqlBuilder.Append($"@p{property.GetIndex()} AS " + property.GetColumnName(storeObject.GetValueOrDefault()) + " ,");

            }

            sqlBuilder.Length -= ",".Length;
            return source.FromSqlRaw(sqlBuilder.ToString(), parameters.ToArray()!);
        }
    }
}
