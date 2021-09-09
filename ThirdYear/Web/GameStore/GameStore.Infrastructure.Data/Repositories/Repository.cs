using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Context;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly GameStoreContext _gameStoreContext;

        public Repository(GameStoreContext gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
        }

        public virtual ICollection<T> GetAll()
        {
            return _gameStoreContext.Set<T>().ToList();
        }

        public virtual ICollection<T> GetAll(Func<T, bool> predicate)
        {
            return _gameStoreContext.Set<T>().Where(predicate).ToList();
        }

        public virtual T GetById(string id)
        {
            return _gameStoreContext.Set<T>().Find(id);
        }

        public void Insert(T item)
        {
            _gameStoreContext.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            _gameStoreContext.Set<T>().Update(item);
        }
    }
}
