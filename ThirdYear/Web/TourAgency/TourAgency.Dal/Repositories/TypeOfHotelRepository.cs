using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class TypeOfHotelRepository : BaseRepository<TypeOfHotel>, ITypeOfHotelRepository
    {
        public TypeOfHotelRepository(TourAgencyContext context) : base(context)
        {
        }
    }
}
