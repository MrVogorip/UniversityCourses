using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models;

namespace GameStore.Application.Filters.Games
{
    public class PublisherFilter : IFilter<Game>
    {
        private readonly ICollection<string> _publishers;

        public PublisherFilter(ICollection<string> publishers)
        {
            _publishers = publishers;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            if (_publishers != null && _publishers.Count() != 0 && !string.IsNullOrEmpty(_publishers.First()))
            {
                Expression<Func<Game, bool>> filter = x => _publishers.Contains(x.Publisher.CompanyName);
                return ToolsExpression.MergeExpression(expression, filter);
            }

            return expression;
        }
    }
}
