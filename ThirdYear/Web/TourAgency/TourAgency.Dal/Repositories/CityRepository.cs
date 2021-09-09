using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(TourAgencyContext context) : base(context)
        {
        }
    }
}
