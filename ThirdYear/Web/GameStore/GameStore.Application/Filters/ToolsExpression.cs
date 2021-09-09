using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.Application.Filters
{
    public static class ToolsExpression
    {
        public static Expression<Func<T, bool>> MergeExpression<T>(
            Expression<Func<T, bool>> expressionFrom,
            Expression<Func<T, bool>> expressionTo)
        {
            var swapper = new Swapper(expressionFrom.Parameters.First(), expressionTo.Parameters.First());

            var expressionNew = swapper.Visit(expressionFrom.Body);

            var result = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(expressionNew, expressionTo.Body),
                expressionTo.Parameters);

            return result;
        }

        private class Swapper : ExpressionVisitor
        {
            private readonly Expression _expressionFrom;
            private readonly Expression _expressionTo;

            public Swapper(Expression expressionFrom, Expression expressionTo)
            {
                _expressionFrom = expressionFrom;
                _expressionTo = expressionTo;
            }

            public override Expression Visit(Expression expressionBody)
            {
                if (expressionBody == _expressionFrom)
                {
                    return _expressionTo;
                }

                return base.Visit(expressionBody);
            }
        }
    }
}
