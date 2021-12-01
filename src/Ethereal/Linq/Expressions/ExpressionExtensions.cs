// Copyright (c) Ethereal. All rights reserved.

using System.Collections.ObjectModel;

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
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));

            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// AndAlso
        /// </summary>
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> first,
            bool condition,
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
            this Expression<Func<T, bool>> first,
            bool condition,
            Expression<Func<T, bool>> second,
            Expression<Func<T, bool>> third)
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
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));

            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>
        /// OrElse
        /// </summary>
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> first,
            bool condition,
            Expression<Func<T, bool>> second)
        {
            Check.NotNull(first, nameof(first));
            Check.NotNull(second, nameof(second));

            return condition ? first.OrElse(second) : first;
        }

        /// <summary>
        /// OrElse
        /// </summary>
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> first,
            bool condition,
            Expression<Func<T, bool>> second,
            Expression<Func<T, bool>> third)
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

        private class ParameterVisitor : ExpressionVisitor
        {
            private readonly ReadOnlyCollection<ParameterExpression> _parameters;

            public ParameterVisitor(ReadOnlyCollection<ParameterExpression> parameters)
                => _parameters = parameters;

            public override Expression? Visit(Expression? node)
                => base.Visit(node);

            protected override Expression VisitParameter(ParameterExpression parameter)
                => _parameters.FirstOrDefault(s => s.Type == parameter.Type) ?? parameter;
        }
    }
}