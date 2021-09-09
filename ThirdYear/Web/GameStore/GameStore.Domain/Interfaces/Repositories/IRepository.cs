using System;
using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        void Insert(T item);

        void Update(T item);

        T GetById(string id);

        ICollection<T> GetAll();

        ICollection<T> GetAll(Func<T, bool> predicate);
    }
}
