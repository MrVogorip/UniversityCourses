using System.Linq;
using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class TourRepository : BaseRepository<Tour>, ITourRepository
    {
        public TourRepository(TourAgencyContext context) : base(context)
        {
        }
        public void UpdateInfo(Tour tour)
        {
            var tourdb = tourAgencyContext.Tours.Find(tour.Id);
            tourdb.StartOfTour = tour.StartOfTour;
            tourdb.EndOfTour = tour.EndOfTour;
            tourdb.TypeOfTourId = tour.TypeOfTourId;
            tourdb.TypeOfHotelId = tour.TypeOfHotelId;
            tourdb.ImagePath = tour.ImagePath;
            tourdb.MaxNumberOfPeople = tour.MaxNumberOfPeople;
            tourdb.Price = tour.Price;
            tourdb.CityId = tour.CityId;
            tourdb.IsHot = tour.IsHot;
            tourdb.NumberOfOrders = tour.NumberOfOrders;
            Update(tourdb);
        }
    }
}
