using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TourAgency.Dal.EF;
using TourAgency.Dal.Entities.Abstractions;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    //Base Repository (generic)
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly TourAgencyContext tourAgencyContext;
        public BaseRepository(TourAgencyContext context)
        {
            tourAgencyContext = context;
        }

        public void Create(T item)
        {
            tourAgencyContext.Set<T>().Add(item);
        }

        public void Delete(int id)
        {
            T item = Get(id);
            if (item != null)
                tourAgencyContext.Set<T>().Remove(item);
        }
        public T Get(int id)
        {
            T item = tourAgencyContext.Set<T>().Find(id);
            if (item != null)
                return item;
            else
                throw new Exception();
        }

        public IEnumerable<T> GetAll()
        {
            return tourAgencyContext.Set<T>().ToList();
        }

        public void Update(T item)
        {
            tourAgencyContext.Entry(item).State = EntityState.Modified;
        }
    }
}
