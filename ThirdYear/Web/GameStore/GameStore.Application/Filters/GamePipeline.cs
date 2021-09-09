using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models;

namespace GameStore.Application.Filters
{
    public class GamePipeline : Pipeline<Game>
    {
        private readonly ICollection<IFilter<Game>> _filters;

        public GamePipeline()
        {
            _filters = new List<IFilter<Game>>();
        }

        public override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> expression)
        {
            foreach (var filter in _filters)
            {
                expression = filter.Execute(expression);
            }

            return expression;
        }

        public GamePipeline Register(IFilter<Game> filter)
        {
            _filters.Add(filter);

            return this;
        }
    }
}
