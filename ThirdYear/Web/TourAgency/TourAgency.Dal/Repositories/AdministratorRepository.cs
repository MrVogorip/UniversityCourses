using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class AdministratorRepository : BaseRepository<Administrator> , IAdministratorRepository
    {
        public AdministratorRepository(TourAgencyContext context) : base(context)
        {
        }
    }
}
