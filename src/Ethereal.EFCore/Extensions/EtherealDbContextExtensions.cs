// Copyright (c) Ethereal. All rights reserved.

using Ethereal.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// EtherealdbContextExtensions
    /// </summary>
    public static class EtherealDbContextExtensions
    {
        /// <summary>
        /// Updates the specified property when the update is true, or do not update
        /// </summary>
        public static EntityEntry<TEntity> Update<TEntity>(
            this DbContext dbContext,
            TEntity entity,
            bool update,
            params string[] properties) where TEntity : class
        {
            Check.NotNull(dbContext, nameof(dbContext));
            Check.NotNull(entity, nameof(entity));
            Check.NotNull(properties, nameof(properties));

            var primaryKey = dbContext.Model.FindRuntimeEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(f => f.Property(primaryKey).OriginalValue.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            var entry = trackingEntity is not null ? trackingEntity : dbContext.Entry(entity);
            entry.State = update ? EntityState.Unchanged : EntityState.Modified;
            foreach (var item in properties)
            {
                entry.Property(item).IsModified = update;
            }
            return entry;
        }

        /// <summary>
        /// Updates the specified property when the update is true, or do not update
        /// </summary>
        public static EntityEntry<TEntity> Update<TEntity>(
            this DbContext dbContext,
            TEntity entity,
            bool update,
            params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            Check.NotNull(dbContext, nameof(dbContext));
            Check.NotNull(entity, nameof(entity));
            Check.NotNull(properties, nameof(properties));

            var primaryKey = dbContext.Model.FindRuntimeEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(f => f.Property(primaryKey).OriginalValue.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            var entry = trackingEntity is not null ? trackingEntity : dbContext.Entry(entity);
            entry.State = update ? EntityState.Unchanged : EntityState.Modified;
            foreach (var item in properties)
            {
                entry.Property(item).IsModified = update;
            }
            return entry;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public static EntityEntry<TEntity> Delete<TEntity>(
            this DbContext dbContext,
            TEntity entity) where TEntity : class, new()
        {
            Check.NotNull(dbContext, nameof(dbContext));
            Check.NotNull(entity, nameof(entity));

            var primaryKey = dbContext.Model.FindRuntimeEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(f => f.Property(primaryKey).OriginalValue.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            var entry = trackingEntity is not null ? trackingEntity : dbContext.Entry(entity);
            entry.State = EntityState.Deleted;
            return entry;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public static EntityEntry<TEntity> Delete<TEntity, TPrimaryKey>(
            this DbContext dbContext,
            TPrimaryKey id) where TEntity : class, new()
                            where TPrimaryKey : notnull
        {
            Check.NotNull(dbContext, nameof(dbContext));

            var primaryKey = dbContext.Model.FindRuntimeEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(f => id.Equals(f.Property(primaryKey).OriginalValue));
            var entry = trackingEntity is not null ? trackingEntity : dbContext.Entry(new TEntity());
            if (trackingEntity is null)
            {
                entry.Property(primaryKey).CurrentValue = id;
            }
            entry.State = EntityState.Deleted;
            return entry;
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <exception cref="InvalidOperationException">SoftDeleteAttribute is not defined</exception>
        public static EntityEntry<TEntity> SoftDelete<TEntity>(
            this DbContext dbContext,
            TEntity entity) where TEntity : class, new()
        {
            Check.NotNull(dbContext, nameof(dbContext));
            Check.NotNull(entity, nameof(entity));

            if (!Attribute.IsDefined(typeof(TEntity), typeof(SoftDeleteAttribute)))
            {
                throw new InvalidOperationException(CoreStrings.SoftDeleted_Invalid);
            }

            var primaryKey = dbContext.Model.FindRuntimeEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(f => f.Property(primaryKey).OriginalValue.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            var entry = trackingEntity is not null ? trackingEntity : dbContext.Entry(entity);
            var isDeleted = typeof(TEntity).GetCustomAttribute<SoftDeleteAttribute>()!.ColumnName;
            entry.Property(isDeleted).IsModified = true;
            entry.Property(isDeleted).CurrentValue = true;
            return entry;
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <exception cref="InvalidOperationException">SoftDeleteAttribute is not defined</exception>
        public static EntityEntry<TEntity> SoftDelete<TEntity, TPrimaryKey>(
            this DbContext dbContext,
            TPrimaryKey id) where TEntity : class, new()
                            where TPrimaryKey : notnull
        {
            Check.NotNull(dbContext, nameof(dbContext));

            if (!Attribute.IsDefined(typeof(TEntity), typeof(SoftDeleteAttribute)))
            {
                throw new InvalidOperationException(CoreStrings.SoftDeleted_Invalid);
            }

            var primaryKey = dbContext.Model.FindRuntimeEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(s => id.Equals(s.Property(primaryKey).OriginalValue));
            var entry = trackingEntity is not null ? trackingEntity : dbContext.Entry(new TEntity());
            if (trackingEntity is null)
            {
                entry.Property(primaryKey).CurrentValue = id;
            }
            var isDeleted = typeof(TEntity).GetCustomAttribute<SoftDeleteAttribute>()!.ColumnName;
            entry.Property(isDeleted).IsModified = true;
            entry.Property(isDeleted).CurrentValue = true;
            return entry;
        }
    }
}