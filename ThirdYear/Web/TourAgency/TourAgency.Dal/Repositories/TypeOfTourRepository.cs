using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class TypeOfTourRepository : BaseRepository<TypeOfTour> , ITypeOfTourRepository
    {
        public TypeOfTourRepository(TourAgencyContext context) : base(context)
        {
        }
    }
}
