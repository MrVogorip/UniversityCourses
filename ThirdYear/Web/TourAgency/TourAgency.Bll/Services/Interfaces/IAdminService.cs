using System.Collections.Generic;
using TourAgency.Bll.DTO;

namespace TourAgency.Bll.Services.Interfaces
{
    public interface IAdminService : IManagerService
    {
        void CreateTour(TourDTO model);
        void DeleteTour(int id);
        List<ManagerDTO> GetAllManagers();
        ManagerDTO GetManagerById(int id);
        void BlockManager(int id);
        void UnlockManager(int id);
        void AddHotel(TypeOfHotelDTO typeOfHotelDTO);
        void AddTypeOfTour(TypeOfTourDTO typeOfTourDTO);
        void AddCity(CityDTO cityDTO);
    }
}
