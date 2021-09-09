using TourAgency.Dal.EF;
using System.Linq;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class TypeOfStatusRepository : BaseRepository<TypeOfStatus>, ITypeOfStatusRepository
    {
        public TypeOfStatusRepository(TourAgencyContext context) : base(context)
        {
        }
        public TypeOfStatus Get(string type)
        {
            TypeOfStatus typeOfStatus = tourAgencyContext.TypeOfStatuses.Where(u => u.Type == type).FirstOrDefault();
            return typeOfStatus;
        }
    }
}
