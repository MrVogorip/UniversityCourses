using System;
using System.Linq.Expressions;
using GameStore.Domain.Models;

namespace GameStore.Application.Filters.Games
{
    public class PriceFilter : IFilter<Game>
    {
        private readonly decimal? _priceFrom;
        private readonly decimal? _priceTo;

        public PriceFilter(decimal? priceFrom, decimal? priceTo)
        {
            _priceFrom = priceFrom;
            _priceTo = priceTo;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            if (_priceFrom != null && _priceTo != null)
            {
                Expression<Func<Game, bool>> filter = x => x.Price >= _priceFrom && x.Price <= _priceTo;
                return ToolsExpression.MergeExpression(expression, filter);
            }

            return expression;
        }
    }
}
