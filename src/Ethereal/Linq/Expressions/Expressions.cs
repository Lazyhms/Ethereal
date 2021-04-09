// Copyright (c) Ethereal. All rights reserved.

namespace System.Linq.Expressions
{
    /// <summary>
    /// Expression
    /// </summary>
    public class Expressions
    {
        /// <summary>
        /// False
        /// </summary>
        public static Expression<Func<T, bool>> False<T>() => DefaultExpression<T>.False;

        /// <summary>
        /// True
        /// </summary>
        public static Expression<Func<T, bool>> False<T>(Expression<Func<T, bool>> predicate) =>
            DefaultExpression<T>.False.AndAlso(predicate);

        /// <summary>
        /// True
        /// </summary>
        public static Expression<Func<T, bool>> True<T>() => DefaultExpression<T>.True;

        /// <summary>
        /// True
        /// </summary>
        public static Expression<Func<T, bool>> True<T>(Expression<Func<T, bool>> predicate) =>
            DefaultExpression<T>.True.AndAlso(predicate);

        private class DefaultExpression<T>
        {
            public static readonly Expression<Func<T, bool>> False = predicate => false;
            public static readonly Expression<Func<T, bool>> True = predicate => true;
        }
    }
}