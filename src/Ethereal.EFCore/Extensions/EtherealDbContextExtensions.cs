// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore
{
    /// <summary>
    /// EtherealdbContextExtensions
    /// </summary>
    public static class EtherealDbContextExtensions
    {
        #region Update

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

            var entry = dbContext.Entry(entity);
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
            IEnumerable<string> properties) where TEntity : class
        {
            Check.NotNull(dbContext, nameof(dbContext));
            Check.NotNull(entity, nameof(entity));
            Check.NotNull(properties, nameof(properties));

            var entry = dbContext.Entry(entity);
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

            var entry = dbContext.Entry(entity);
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
        public static EntityEntry<TEntity> Update<TEntity, TProperty>(
            this DbContext dbContext,
            TEntity entity,
            bool update,
            params Expression<Func<TEntity, TProperty>>[] properties) where TEntity : class
        {
            Check.NotNull(dbContext, nameof(dbContext));
            Check.NotNull(entity, nameof(entity));
            Check.NotNull(properties, nameof(properties));

            var entry = dbContext.Entry(entity);
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
        public static EntityEntry<TEntity> WithProperty<TEntity, TProperty>(
            this EntityEntry<TEntity> entry,
            bool update,
            params Expression<Func<TEntity, TProperty>>[] properties) where TEntity : class
        {
            Check.NotNull(entry, nameof(entry));

            foreach (var item in properties)
            {
                entry.Property(item).IsModified = update;
            }
            return entry;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Delete
        /// </summary>
        public static EntityEntry<TEntity> Delete<TEntity>(
            this DbContext dbContext,
            TEntity entity) where TEntity : class, new()
        {
            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Deleted;
            return entry;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public static EntityEntry<TEntity> Delete<TEntity, TPrimaryKey>(
            this DbContext dbContext,
            TPrimaryKey id) where TEntity : class, new()
        {
            var entry = dbContext.Entry(new TEntity());
            entry.Property("Id").CurrentValue = id;
            entry.State = EntityState.Deleted;
            return entry;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public static EntityEntry<TEntity> Delete<TEntity>(
            this DbContext dbContext,
            object id) where TEntity : class, new()
        {
            var entry = dbContext.Entry(new TEntity());
            entry.Property("Id").CurrentValue = id;
            entry.State = EntityState.Deleted;
            return entry;
        }

        #endregion Delete

        #region SoftDelete

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <exception cref="InvalidOperationException">SoftDeleteAttribute is not defined</exception>
        public static EntityEntry<TEntity> SoftDelete<TEntity>(
            this DbContext dbContext,
            TEntity entity) where TEntity : class, new()
        {
            if (!Attribute.IsDefined(typeof(TEntity), typeof(SoftDeleteAttribute)))
            {
                throw new InvalidOperationException(CoreStrings.SoftDeleted_Invalid);
            }
            var entry = dbContext.Entry(entity);
            var isDeleted = typeof(TEntity).GetCustomAttribute<SoftDeleteAttribute>()!.IsDeleted;
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
        {
            if (!Attribute.IsDefined(typeof(TEntity), typeof(SoftDeleteAttribute)))
            {
                throw new InvalidOperationException(CoreStrings.SoftDeleted_Invalid);
            }
            var entry = dbContext.Entry(new TEntity());
            entry.Property("Id").CurrentValue = id;
            var isDeleted = typeof(TEntity).GetCustomAttribute<SoftDeleteAttribute>()!.IsDeleted;
            entry.State = EntityState.Unchanged;
            entry.Property(isDeleted).IsModified = true;
            entry.Property(isDeleted).CurrentValue = true;
            return entry;
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <exception cref="InvalidOperationException">SoftDeleteAttribute is not defined</exception>
        public static EntityEntry<TEntity> SoftDelete<TEntity>(
            this DbContext dbContext,
            object id) where TEntity : class, new()
        {
            if (!Attribute.IsDefined(typeof(TEntity), typeof(SoftDeleteAttribute)))
            {
                throw new InvalidOperationException(CoreStrings.SoftDeleted_Invalid);
            }
            var entry = dbContext.Entry(new TEntity());
            entry.Property("Id").CurrentValue = id;
            var isDeleted = typeof(TEntity).GetCustomAttribute<SoftDeleteAttribute>()!.IsDeleted;
            entry.State = EntityState.Unchanged;
            entry.Property(isDeleted).IsModified = true;
            entry.Property(isDeleted).CurrentValue = true;
            return entry;
        }

        #endregion SoftDelete
    }
}