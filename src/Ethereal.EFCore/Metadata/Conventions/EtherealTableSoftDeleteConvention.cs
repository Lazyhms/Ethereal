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
    public class EtherealTableSoftDeleteConvention : IEntityTypeAddedConvention
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
                if (clrType.GetProperty(attribute.IsDeleted) is PropertyInfo info)
                {
                    if (info.PropertyType != typeof(bool))
                    {
                        throw new TypeAccessException(string.Format(CoreStrings.SoftDeletedType_Invalid, info.Name));
                    }
                    entityTypeBuilder.Property(info.PropertyType, info.Name, true).HasComment(attribute.Comment, true);
                }
                else
                {
                    entityTypeBuilder.Property(typeof(bool), attribute.IsDeleted, true).HasComment(attribute.Comment, true);
                }
                // add query filter
                var parameter = Expression.Parameter(clrType, "filter");
                entityTypeBuilder.HasQueryFilter(
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter,
                                Expression.Constant(attribute.IsDeleted)), Expression.Constant(false)), parameter),
                    true);
            }
        }
    }
}