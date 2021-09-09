using System.Linq;
using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(TourAgencyContext context) : base(context)
        {
        }
        public int GetCManagerIdByIdentityUserId(string userId)
        {
            var manager = tourAgencyContext.Managers.Where(u => u.UserId == userId).FirstOrDefault();
            if (manager is null)
                return -1;
            return manager.Id;
        }
        public void Register(Manager manager)
        {
            tourAgencyContext.Managers.Add(manager);
        }
    }
}
