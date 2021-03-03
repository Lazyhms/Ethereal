// Copyright (c) Ethereal. All rights reserved.
//

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnUpdateIgnoreConvention
    /// </summary>
    public class EtherealColumnUpdateIgnoreConvention : PropertyAttributeConventionBase<UpdateIgnoreAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnUpdateIgnoreConvention"/> class.
        /// </summary>
        public EtherealColumnUpdateIgnoreConvention([NotNull] ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            [NotNull] IConventionPropertyBuilder propertyBuilder,
            [NotNull] UpdateIgnoreAttribute attribute,
            [NotNull] MemberInfo clrMember,
            [NotNull] IConventionContext context)
        {
            if (propertyBuilder.CanSetAfterSave(PropertySaveBehavior.Ignore, true))
            {
                propertyBuilder.AfterSave(PropertySaveBehavior.Ignore, true);
            }
        }
    }
}
