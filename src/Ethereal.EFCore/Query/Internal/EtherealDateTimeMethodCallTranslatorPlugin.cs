// Copyright (c) Ethereal. All rights reserved.
//

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    /// <summary>
    /// DateTimeMethodCallTranslatorPlugin
    /// </summary>
    public class EtherealDateTimeMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EtherealDateTimeMethodCallTranslatorPlugin"/> class.
        /// </summary>
        public EtherealDateTimeMethodCallTranslatorPlugin(
            [NotNull] ISqlExpressionFactory sqlExpressionFactory,
            [NotNull] IRelationalTypeMappingSource typeMappingSource) =>
            Translators = new IMethodCallTranslator[] {
                new EtherealDateTimeMethodTranslator(sqlExpressionFactory,typeMappingSource)
            };

        /// <inheritdoc/>
        public IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
