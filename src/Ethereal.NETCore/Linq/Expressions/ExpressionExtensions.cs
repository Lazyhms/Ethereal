// Copyright (c) Ethereal. All rights reserved.
//

using Ethereal.Utilities;
using JetBrains.Annotations;
using System.Linq.Expressions.Internal;

namespace System.Linq.Expressions
{
    /// <summary>
    /// ExpressionExtensions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// AndAlso
        /// </summary>
        public static Expression<Func<T, bool>> AndAlso<T>([
            NotNull] this Expression<Func<T, bool>> first,
            [NotNull] Expression<Func<T, bool>> second)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));

            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// AndAlso
        /// </summary>
        public static Expression<Func<T, bool>> AndAlso<T>(
            [NotNull] this Expression<Func<T, bool>> first,
            bool condition, [NotNull]
            Expression<Func<T, bool>> second)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));

            return condition ? first.AndAlso(second) : first;
        }

        /// <summary>
        /// AndAlso
        /// </summary>
        public static Expression<Func<T, bool>> AndAlso<T>(
            [NotNull] this Expression<Func<T, bool>> first,
            bool condition,
            [NotNull] Expression<Func<T, bool>> second,
            [NotNull] Expression<Func<T, bool>> third)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));
            Check.NotNull(second, nameof(third));

            return condition ? first.AndAlso(second) : first.AndAlso(third);
        }

        /// <summary>
        /// OrElse
        /// </summary>
        public static Expression<Func<T, bool>> OrElse<T>(
            [NotNull] this Expression<Func<T, bool>> first,
            [NotNull] Expression<Func<T, bool>> second)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));

            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>
        /// OrElse
        /// </summary>
        public static Expression<Func<T, bool>> OrElse<T>(
            [NotNull] this Expression<Func<T, bool>> first,
            bool condition,
            [NotNull] Expression<Func<T, bool>> second)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));

            return condition ? first.OrElse(second) : first;
        }

        /// <summary>
        /// OrElse
        /// </summary>
        public static Expression<Func<T, bool>> OrElse<T>(
            [NotNull] this Expression<Func<T, bool>> first,
            bool condition,
            [NotNull] Expression<Func<T, bool>> second,
            [NotNull] Expression<Func<T, bool>> third)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));
            Check.NotNull(third, nameof(third));

            return condition ? first.OrElse(second) : first.OrElse(third);
        }

        private static Expression<T> Compose<T>(
            this Expression<T> first,
            Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var visitor = new ParameterVisitor(first.Parameters);
            var expression = merge(visitor.Visit(first.Body)!, visitor.Visit(second.Body)!);
            return Expression.Lambda<T>(expression, first.Parameters);
        }
    }
}
