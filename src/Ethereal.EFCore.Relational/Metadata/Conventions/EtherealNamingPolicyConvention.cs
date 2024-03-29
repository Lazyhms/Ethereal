﻿// Copyright (c) Ethereal. All rights reserved.

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

                var storeObjectIdentifier = StoreObjectTypes.Select(storeObjectType => StoreObjectIdentifier.Create(declaringEntityType, storeObjectType))
                    .FirstOrDefault(storeObjectIdentifier => storeObjectIdentifier is not null);

                foreach (var property in declaringEntityType.GetProperties())
                {
                    property.Builder.HasColumnName(RewriteNamingCase(property.GetColumnName(storeObjectIdentifier.GetValueOrDefault())));
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
                if (entityTypeBuilder.Metadata.GetTableNameConfigurationSource() is not null)
                {
                    entityTypeBuilder.ToTable(RewriteNamingCase(entityTypeBuilder.Metadata.GetTableName()), true);

                    if (entityTypeBuilder.Metadata.GetSchemaConfigurationSource() is not null)
                    {
                        entityTypeBuilder.ToSchema(RewriteNamingCase(entityTypeBuilder.Metadata.GetSchema()), true);
                    }
                }
                else if (entityTypeBuilder.Metadata.GetViewNameConfigurationSource() is not null)
                {
                    entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewName()));

                    if (entityTypeBuilder.Metadata.GetViewSchemaConfigurationSource() is not null)
                    {
                        entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewSchema()));
                    }
                }
                else if (entityTypeBuilder.Metadata.GetFunctionNameConfigurationSource() is not null)
                {
                    entityTypeBuilder.ToFunction(RewriteNamingCase(entityTypeBuilder.Metadata.GetFunctionName()));
                }
            }
        }

        /// <inheritdoc/>
        public void ProcessPropertyAdded(
            IConventionPropertyBuilder propertyBuilder,
            IConventionContext<IConventionPropertyBuilder> context)
        {
            var storeObjectIdentifier = StoreObjectTypes.Select(storeObjectType => StoreObjectIdentifier.Create(propertyBuilder.Metadata.DeclaringEntityType, storeObjectType))
                .FirstOrDefault(storeObjectIdentifier => storeObjectIdentifier is not null);

            propertyBuilder.HasColumnName(RewriteNamingCase(propertyBuilder.Metadata.GetColumnName(storeObjectIdentifier.GetValueOrDefault())), true);
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
            => indexBuilder.HasDatabaseName(RewriteNamingCase(indexBuilder.Metadata.GetDatabaseName()), true);

        /// <inheritdoc/>
        public void ProcessKeyAdded(
            IConventionKeyBuilder keyBuilder,
            IConventionContext<IConventionKeyBuilder> context)
            => keyBuilder.HasName(RewriteNamingCase(keyBuilder.Metadata.GetName()), true);

        /// <inheritdoc/>
        public void ProcessEntityTypeBaseTypeChanged(
            IConventionEntityTypeBuilder entityTypeBuilder,
            IConventionEntityType? newBaseType,
            IConventionEntityType? oldBaseType,
            IConventionContext<IConventionEntityType> context)
        {
            if (newBaseType is null)
            {
                if (entityTypeBuilder.Metadata.GetTableNameConfigurationSource() is not null)
                {
                    entityTypeBuilder.ToTable(RewriteNamingCase(entityTypeBuilder.Metadata.GetTableName()), true);

                    if (entityTypeBuilder.Metadata.GetSchemaConfigurationSource() is not null)
                    {
                        entityTypeBuilder.ToSchema(RewriteNamingCase(entityTypeBuilder.Metadata.GetSchema()), true);
                    }
                }
                else if (entityTypeBuilder.Metadata.GetViewNameConfigurationSource() is not null)
                {
                    entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewName()));

                    if (entityTypeBuilder.Metadata.GetViewSchemaConfigurationSource() is not null)
                    {
                        entityTypeBuilder.ToView(RewriteNamingCase(entityTypeBuilder.Metadata.GetViewSchema()));
                    }
                }
                else if (entityTypeBuilder.Metadata.GetFunctionNameConfigurationSource() is not null)
                {
                    entityTypeBuilder.ToFunction(RewriteNamingCase(entityTypeBuilder.Metadata.GetFunctionName()));
                }
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
                var storeObjectIdentifier = StoreObjectTypes.Select(storeObjectType => StoreObjectIdentifier.Create(propertyBuilder.Metadata.DeclaringEntityType, storeObjectType))
                    .FirstOrDefault(storeObjectIdentifier => storeObjectIdentifier is not null);

                propertyBuilder.HasColumnName(RewriteNamingCase(propertyBuilder.Metadata.GetColumnName(storeObjectIdentifier.GetValueOrDefault())));
            }
        }

        /// <summary>
        /// Rewrite naming
        /// </summary>
        protected virtual string? RewriteNamingCase(string? name)
            => NamingPolicy switch
            {
                NamingPolicy.LowerCase => name?.ToLower(),
                NamingPolicy.UpperCase => name?.ToUpper(),
                _ => name,
            };
    }
}
