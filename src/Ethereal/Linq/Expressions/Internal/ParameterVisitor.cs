// Copyright (c) Ethereal. All rights reserved.

using System.Collections.ObjectModel;

namespace System.Linq.Expressions.Internal
{
    internal class ParameterVisitor : ExpressionVisitor
    {
        private readonly ReadOnlyCollection<ParameterExpression> _parameters;

        public ParameterVisitor(ReadOnlyCollection<ParameterExpression> parameters) =>
            _parameters = parameters;

        public override Expression? Visit(Expression? node) => base.Visit(node);

        protected override Expression VisitParameter(ParameterExpression parameter) =>
            _parameters.FirstOrDefault(s => s.Type == parameter.Type) ?? parameter;
    }
}