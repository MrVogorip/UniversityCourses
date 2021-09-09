using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Helpers;

namespace TourAgency.Dal.EF
{
    //Context database
    public class TourAgencyContext : IdentityDbContext<IdentityUser>
    {
        public TourAgencyContext(string connectionString) 
            : base(connectionString)
        {
        }
        static TourAgencyContext()
        {
            Database.SetInitializer<TourAgencyContext>(new TourAgencyDbInitializer());
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TypeOfStatus> TypeOfStatuses { get; set; }
        public DbSet<TypeOfHotel> TypeOfHotels { get; set; }
        public DbSet<TypeOfTour> TypeOfTours { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourCustomer> TourCustomers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
