using TourAgency.Dal.Entities;

namespace TourAgency.Dal.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        void Register(Customer model);
        int GetCustomerIdByIdentityUserId(string userId);
        void UpdateInfo(Customer model);
    }
}
