using System.Linq;
using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TourAgencyContext context) : base(context)
        {
        }
        public int GetCustomerIdByIdentityUserId(string userId)
        {
            var customer = tourAgencyContext.Customers.Where(u => u.UserId == userId).FirstOrDefault();
            if(customer is null)
                return -1;
            return customer.Id;
        }

        public void UpdateInfo(Customer model)
        {
            var customer = tourAgencyContext.Customers.Where(u => u.Id == model.Id).FirstOrDefault();
            if(customer != null)
            {
                customer.IsBlock = model.IsBlock;
                customer.Discount = model.Discount;
                customer.MaxDiscount = model.MaxDiscount;
                customer.StepDiscount = model.StepDiscount;
                customer.UserId = model.UserId;
                Update(customer);
            }
        }

        public void Register(Customer customer)
        {
            tourAgencyContext.Customers.Add(customer);
        }
    }
}
