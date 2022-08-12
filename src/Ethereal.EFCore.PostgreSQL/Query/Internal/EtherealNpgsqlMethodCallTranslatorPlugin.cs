// Copyright (c) Ethereal. All rights reserved.

using Microsoft.EntityFrameworkCore.Query;

namespace Ethereal.EFCore.PostgreSQL.Query.Internal
{
    /// <summary>
    /// 1
    /// </summary>
    public class EtherealNpgsqlMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        /// <summary>
        /// 1
        /// </summary>
        public EtherealNpgsqlMethodCallTranslatorPlugin(
            ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new IMethodCallTranslator[]
            {
                new EtherealNpgsqlIEnumerableDbFunctionsTranslator(sqlExpressionFactory)
            };
        }

        /// <inheritdoc/>
        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
