using System;
using System.Collections.Generic;

namespace TourAgency.Dal.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
