// Copyright (c) Ethereal. All rights reserved.
//

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System.Reflection;

namespace Ethereal.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    /// EtherealColumnSequenceConvention
    /// </summary>
    public class EtherealColumnSequenceValueConvention : PropertyAttributeConventionBase<SequenceValueAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealColumnSequenceValueConvention"/> class.
        /// </summary>
        public EtherealColumnSequenceValueConvention([NotNull] ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
        {
        }

        /// <inheritdoc/>
        protected override void ProcessPropertyAdded(
            [NotNull] IConventionPropertyBuilder propertyBuilder,
            [NotNull] SequenceValueAttribute attribute,
            [NotNull] MemberInfo clrMember,
            [NotNull] IConventionContext context)
        {
            var sequence = $"NEXT VALUE FOR {(string.IsNullOrWhiteSpace(attribute.Schema) ? string.Empty : $"[{attribute.Schema}].")} [{attribute.Name}]";
            if (propertyBuilder.CanSetDefaultValueSql(sequence, true))
            {
                propertyBuilder.HasDefaultValueSql(sequence, true);
            }
        }
    }
}
