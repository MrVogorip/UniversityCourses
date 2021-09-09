using TourAgency.Dal.Entities;

namespace TourAgency.Dal.Repositories.Interfaces
{
    public interface ITourRepository
    {
        void UpdateInfo(Tour tour);
    }
}
