// Copyright (c) Ethereal. All rights reserved.
//

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealTableQueryFilterConvention
    /// </summary>
    public class EtherealTableQueryFilterConvention : PropertyAttributeConventionBase<QueryFilterAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealTableQueryFilterConvention"/> class.
        /// </summary>
        public EtherealTableQueryFilterConvention([NotNull] ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            [NotNull] IConventionPropertyBuilder propertyBuilder,
            [NotNull] QueryFilterAttribute attribute,
            [NotNull] MemberInfo clrMember,
            [NotNull] IConventionContext context)
        {
            var clrEntityType = propertyBuilder.Metadata.DeclaringEntityType.ClrType;
            var clrType = clrMember switch
            {
                PropertyInfo propertyInfo => propertyInfo.PropertyType,
                FieldInfo fieldInfo => fieldInfo.FieldType,
                _ => null,
            };
            var entityTypeBuilder = propertyBuilder.Metadata.DeclaringEntityType.Builder;
            var queryFilter = propertyBuilder.Metadata.DeclaringEntityType.GetQueryFilter();
            if (queryFilter is null)
            {
                var parameter = Expression.Parameter(clrEntityType, "filter");
                entityTypeBuilder.HasQueryFilter(
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Call(typeof(EF), nameof(EF.Property), clrType == null ? null : new Type[] { clrType! }, parameter, Expression.Constant(clrMember.Name)),
                            Expression.Constant(false)),
                        parameter),
                    true);
            }
            else
            {
                entityTypeBuilder.HasQueryFilter(
                    Expression.Lambda(
                        Expression.AndAlso(queryFilter.Body,
                            Expression.Equal(
                                Expression.Call(typeof(EF), nameof(EF.Property), clrType == null ? null : new Type[] { clrType! }, queryFilter.Parameters.Union(new Expression[] { Expression.Constant(clrMember?.Name) }).ToArray()),
                                Expression.Constant(attribute.Value))
                            ), queryFilter.Parameters),
                    true);
            }
        }
    }
}
