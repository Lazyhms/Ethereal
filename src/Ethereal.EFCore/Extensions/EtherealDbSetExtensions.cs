// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore
{
    /// <summary>
    /// EtherealDbSetExtensions
    /// </summary>
    public static class EtherealDbSetExtensions
    {
        /// <summary>
        /// Updates the specified property when the update is true, or do not update
        /// </summary>
        public static EntityEntry<TEntity> Update<TEntity>(
            this DbSet<TEntity> dbSet,
            TEntity entity,
            bool update,
            params string[] properties) where TEntity : class
        {
            Check.NotNull(dbSet, nameof(dbSet));
            Check.NotNull(entity, nameof(entity));
            Check.NotNull(properties, nameof(properties));

            var primaryKey = dbSet.EntityType.FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbSet.Local.FirstOrDefault(s => s.GetType().GetRequiredRuntimePropertyValue(primaryKey, s)!.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            var entry = dbSet.Attach(trackingEntity ?? entity);
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
            this DbSet<TEntity> dbSet,
            TEntity entity,
            bool update,
            params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            Check.NotNull(dbSet, nameof(dbSet));
            Check.NotNull(entity, nameof(entity));
            Check.NotNull(properties, nameof(properties));

            var primaryKey = dbSet.EntityType.FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbSet.Local.FirstOrDefault(s => s.GetType().GetRequiredRuntimePropertyValue(primaryKey, s)!.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            var entry = dbSet.Attach(trackingEntity ?? entity);
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
            this DbSet<TEntity> dbSet,
            TEntity entity) where TEntity : class, new()
        {
            Check.NotNull(dbSet, nameof(dbSet));
            Check.NotNull(entity, nameof(entity));

            var primaryKey = dbSet.EntityType.FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbSet.Local.FirstOrDefault(s => s.GetType().GetRequiredRuntimePropertyValue(primaryKey, s)!.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            return dbSet.Remove(trackingEntity ?? entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        public static EntityEntry<TEntity> Delete<TEntity, TPrimaryKey>(
            this DbSet<TEntity> dbSet,
            TPrimaryKey id) where TEntity : class, new()
                            where TPrimaryKey : notnull
        {
            Check.NotNull(dbSet, nameof(dbSet));

            var primaryKey = dbSet.EntityType.FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbSet.Local.FirstOrDefault(s => id.Equals(s.GetType().GetRequiredRuntimePropertyValue(primaryKey, s)));
            var entry = dbSet.Attach(trackingEntity ?? new TEntity());
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
            this DbSet<TEntity> dbSet,
            TEntity entity) where TEntity : class, new()
        {
            Check.NotNull(dbSet, nameof(dbSet));
            Check.NotNull(entity, nameof(entity));

            if (!Attribute.IsDefined(typeof(TEntity), typeof(SoftDeleteAttribute)))
            {
                throw new InvalidOperationException(CoreStrings.SoftDeleted_Invalid);
            }

            var primaryKey = dbSet.EntityType.FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbSet.Local.FirstOrDefault(s => s.GetType().GetRequiredRuntimePropertyValue(primaryKey, s)!.Equals(typeof(TEntity).GetRequiredRuntimePropertyValue(primaryKey, entity)));
            var entry = dbSet.Attach(trackingEntity ?? entity);
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
            this DbSet<TEntity> dbSet,
            TPrimaryKey id) where TEntity : class, new()
                            where TPrimaryKey : notnull
        {
            Check.NotNull(dbSet, nameof(dbSet));

            if (!Attribute.IsDefined(typeof(TEntity), typeof(SoftDeleteAttribute)))
            {
                throw new InvalidOperationException(CoreStrings.SoftDeleted_Invalid);
            }

            var primaryKey = dbSet.EntityType.FindPrimaryKey().Properties.Select(s => s.Name).Single();
            var trackingEntity = dbSet.Local.FirstOrDefault(s => id.Equals(s.GetType().GetRequiredRuntimePropertyValue("Id", s)));
            var entry = dbSet.Attach(trackingEntity ?? new TEntity());
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