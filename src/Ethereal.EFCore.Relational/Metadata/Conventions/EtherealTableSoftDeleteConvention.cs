// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealTableSoftDeleteConvention
    /// </summary>
    public sealed class EtherealTableSoftDeleteConvention : IEntityTypeAddedConvention
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealTableSoftDeleteConvention"/> class.
        /// </summary>
        public EtherealTableSoftDeleteConvention()
        {
        }

        /// <inheritdoc/>
        public void ProcessEntityTypeAdded(
            IConventionEntityTypeBuilder entityTypeBuilder,
            IConventionContext<IConventionEntityTypeBuilder> context)
        {
            var clrType = entityTypeBuilder.Metadata.ClrType;
            if (clrType is not null && Attribute.IsDefined(clrType, typeof(SoftDeleteAttribute)))
            {
                var attribute = clrType.GetCustomAttribute<SoftDeleteAttribute>()!;
                entityTypeBuilder.Property(typeof(bool), attribute.ColumnName, true)!
                                 .HasComment(attribute.Comment, true)!
                                 .HasDefaultValue(false);
                // add query filter
                var parameter = Expression.Parameter(clrType);
                entityTypeBuilder.HasQueryFilter(
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Call(
                                        typeof(EF),
                                        nameof(EF.Property),
                                        new[] { typeof(bool) },
                                        parameter, Expression.Constant(attribute.ColumnName)),
                            Expression.Constant(false)),
                        parameter),
                    true);
            }
        }
    }
}