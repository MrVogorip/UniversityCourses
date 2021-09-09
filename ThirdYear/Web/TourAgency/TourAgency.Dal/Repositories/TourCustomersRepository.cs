using System.Linq;
using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class TourCustomersRepository : BaseRepository<TourCustomer>, ITourCustomersRepository
    {
        public TourCustomersRepository(TourAgencyContext context) : base(context)
        {
        }
        public void SetStatus(int id, int idStatus)
        {
            var tourCustomer = tourAgencyContext.TourCustomers.Find(id);
            var typeOfStatus = tourAgencyContext.TypeOfStatuses.Where(u => u.Id == idStatus).FirstOrDefault();
            tourCustomer.TypeOfStatus = typeOfStatus;
            tourCustomer.TypeOfStatusId = typeOfStatus.Id;
            Update(tourCustomer);
        }
    }
}
