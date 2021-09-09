using System;
using System.Linq.Expressions;

namespace GameStore.Application.Filters
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> Execute(Expression<Func<T, bool>> expression);
    }
}
