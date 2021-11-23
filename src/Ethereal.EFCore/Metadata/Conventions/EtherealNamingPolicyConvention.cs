// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// Naming policy
    /// </summary>
    public class EtherealNamingPolicyConvention :
        IEntityTypeAddedConvention,
        IEntityTypeBaseTypeChangedConvention,
        IKeyAddedConvention,
        IIndexAddedConvention,
        IPropertyAddedConvention,
        IForeignKeyAddedConvention,
        IPropertyFieldChangedConvention,
        IForeignKeyOwnershipChangedConvention
    {
        /// <summary>
        /// Naming policy
        /// </summary>
        protected NamingPolicy NamingPolicy { get; }

        /// <summary>
        /// StoreObjectTypes
        /// </summary>
        protected IReadOnlyCollection<StoreObjectType> StoreObjectTypes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealNamingPolicyConvention"/> class.
        /// </summary>
        public EtherealNamingPolicyConvention(NamingPolicy namingPolicy)
        {
            NamingPolicy = namingPolicy;
            StoreObjectTypes = Enum.GetValues<StoreObjectType>();
        }

        /// <inheritdoc/>
        public void ProcessForeignKeyOwnershipChanged(
            IConventionForeignKeyBuilder relationshipBuilder,
            IConventionContext<bool?> context)
        {
            var declaringEntityType = relationshipBuilder.Metadata.DeclaringEntityType;
            if (relationshipBuilder.Metadata.IsOwnership)
            {
                declaringEntityType.Builder.HasNoAnnotation(RelationalAnnotationNames.TableName);
                declaringEntityType.Builder.HasNoAnnotation(RelationalAnnotationNames.Schema);
                declaringEntityType.FindPrimaryKey()?.Builder.HasNoAnnotation(RelationalAnnotationNames.Name);

                foreach (var property in declaringEntityType.GetProperties())
                {
                    foreach (var store in from storeObjectType in StoreObjectTypes
                                          let store = StoreObjectIdentifier.Create(property.DeclaringEntityType, storeObjectType)
                                          where store is not null
                                          select store)
                    {
                        property.Builder.HasColumnName(RewriteNamingCase(property.GetColumnName(store.GetValueOrDefault())));
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void ProcessEntityTypeAdded(
            IConventionEntityTypeBuilder entityTypeBuilder,
            IConventionContext<IConventionEntityTypeBuilder> context)
        {
            if (entityTypeBuilder.Metadata.BaseType is null)
            {
                if (entityTypeBuilder.Metadata.GetTableNameConfigurationSource() == ConfigurationSource.DataAnnotation)
                {
                    entityTypeBuilder.ToTable(RewriteNamingCase(entityTypeBuilder.Metadata.GetTableName()));
                }
                if (entityTypeBuilder.Metadata.GetSchemaConfigurationSource() == ConfigurationSource.DataAnnotation)
                {
                    entityTypeBuilder.ToSchema(RewriteNamingCase(entityTypeBuilder.Metadata.GetSchema()));
                }
                if (entityTypeBuilder.Metadata.GetViewNameConfigurationSource() == ConfigurationSource.Convention)
                {
                    entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewName()));
                }
                if (entityTypeBuilder.Metadata.GetViewSchemaConfigurationSource() == ConfigurationSource.Convention)
                {
                    entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewSchema()));
                }
            }
        }

        /// <inheritdoc/>
        public void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            IConventionContext<IConventionPropertyBuilder> context)
        {
            foreach (var store in from storeObjectType in StoreObjectTypes
                                  let store = StoreObjectIdentifier.Create(propertyBuilder.Metadata.DeclaringEntityType, storeObjectType)
                                  where store is not null
                                  select store)
            {
                propertyBuilder.HasColumnName(RewriteNamingCase(propertyBuilder.Metadata.GetColumnName(store.GetValueOrDefault())));
            }
        }

        /// <inheritdoc/>
        public void ProcessForeignKeyAdded(
            IConventionForeignKeyBuilder foreignKeyBuilder,
            IConventionContext<IConventionForeignKeyBuilder> context)
            => foreignKeyBuilder.HasConstraintName(RewriteNamingCase(foreignKeyBuilder.Metadata.GetConstraintName()));

        /// <inheritdoc/>
        public void ProcessIndexAdded(
            IConventionIndexBuilder indexBuilder,
            IConventionContext<IConventionIndexBuilder> context)
            => indexBuilder.HasDatabaseName(RewriteNamingCase(indexBuilder.Metadata.GetDatabaseName()));

        /// <inheritdoc/>
        public void ProcessKeyAdded(
            IConventionKeyBuilder keyBuilder,
            IConventionContext<IConventionKeyBuilder> context)
            => keyBuilder.HasName(RewriteNamingCase(keyBuilder.Metadata.GetName()));

        /// <inheritdoc/>
        public void ProcessEntityTypeBaseTypeChanged(
            IConventionEntityTypeBuilder entityTypeBuilder,
            IConventionEntityType? newBaseType,
            IConventionEntityType? oldBaseType,
            IConventionContext<IConventionEntityType> context)
        {
            if (newBaseType is null)
            {
                if (entityTypeBuilder.Metadata.GetTableNameConfigurationSource() == ConfigurationSource.DataAnnotation)
                {
                    entityTypeBuilder.ToTable(RewriteNamingCase(entityTypeBuilder.Metadata.GetTableName()));
                }
                if (entityTypeBuilder.Metadata.GetSchemaConfigurationSource() == ConfigurationSource.DataAnnotation)
                {
                    entityTypeBuilder.ToSchema(RewriteNamingCase(entityTypeBuilder.Metadata.GetSchema()));
                }
                if (entityTypeBuilder.Metadata.GetViewNameConfigurationSource() == ConfigurationSource.Convention)
                {
                    entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewName()));
                }
                if (entityTypeBuilder.Metadata.GetViewSchemaConfigurationSource() == ConfigurationSource.Convention)
                {
                    entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewSchema()));
                }
            }
            else
            {
                entityTypeBuilder.HasNoAnnotation(RelationalAnnotationNames.TableName);
                entityTypeBuilder.HasNoAnnotation(RelationalAnnotationNames.Schema);
            }
        }

        /// <inheritdoc/>
        public void ProcessPropertyFieldChanged(
            IConventionPropertyBuilder propertyBuilder,
            FieldInfo? newFieldInfo,
            FieldInfo? oldFieldInfo,
            IConventionContext<FieldInfo> context)
        {
            if (newFieldInfo is null)
            {
                foreach (var store in from storeObjectType in StoreObjectTypes
                                      let store = StoreObjectIdentifier.Create(propertyBuilder.Metadata.DeclaringEntityType, storeObjectType)
                                      where store is not null
                                      select store)
                {
                    propertyBuilder.HasColumnName(RewriteNamingCase(propertyBuilder.Metadata.GetColumnName(store.GetValueOrDefault())));
                }
            }
            else
            {
                propertyBuilder.HasNoAnnotation(RelationalAnnotationNames.TableName);
                propertyBuilder.HasNoAnnotation(RelationalAnnotationNames.Schema);
            }
        }

        /// <summary>
        /// Rewrite naming
        /// </summary>
        protected virtual string? RewriteNamingCase(string? name)
            => NamingPolicy switch
            {
                NamingPolicy.LOWERCASE => name?.ToLower(),
                NamingPolicy.UPPERCASE => name?.ToUpper(),
                _ => name,
            };
    }
}
