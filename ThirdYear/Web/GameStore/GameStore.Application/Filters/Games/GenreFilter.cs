using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models;

namespace GameStore.Application.Filters.Games
{
    public class GenreFilter : IFilter<Game>
    {
        private readonly ICollection<string> _genres;

        public GenreFilter(ICollection<string> genres)
        {
            _genres = genres;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            if (_genres != null && _genres.Count() != 0 && !string.IsNullOrEmpty(_genres.First()))
            {
                Expression<Func<Game, bool>> filter = x => x.GameGenres.Any(y => _genres.Contains(y.Genre.Name));
                return ToolsExpression.MergeExpression(expression, filter);
            }

            return expression;
        }
    }
}
