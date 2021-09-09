using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Models;

namespace GameStore.Application.Filters.Games
{
    public class PlatformFilter : IFilter<Game>
    {
        private readonly ICollection<string> _platforms;

        public PlatformFilter(ICollection<string> platforms)
        {
            _platforms = platforms;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            if (_platforms != null && _platforms.Count() != 0 && !string.IsNullOrEmpty(_platforms.First()))
            {
                Expression<Func<Game, bool>> filter = x => x.GamePlatforms.Any(y => _platforms.Contains(y.Platform.Name));
                return ToolsExpression.MergeExpression(expression, filter);
            }

            return expression;
        }
    }
}
