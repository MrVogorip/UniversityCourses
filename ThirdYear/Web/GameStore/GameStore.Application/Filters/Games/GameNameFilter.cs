using System;
using System.Linq.Expressions;
using GameStore.Domain.Models;

namespace GameStore.Application.Filters.Games
{
    public class GameNameFilter : IFilter<Game>
    {
        private readonly string _gameName;

        public GameNameFilter(string gameName)
        {
            _gameName = gameName;
        }

        public Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> expression)
        {
            if (_gameName != null)
            {
                Expression<Func<Game, bool>> filter = x => x.Name.Contains(_gameName);
                return ToolsExpression.MergeExpression(expression, filter);
            }

            return expression;
        }
    }
}
