using System;
using System.Linq.Expressions;

namespace GameStore.Application.Filters
{
    public abstract class Pipeline<T>
    {
        public abstract Expression<Func<T, bool>> Process(Expression<Func<T, bool>> expression);
    }
}
