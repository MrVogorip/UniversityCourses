using System.Collections.Generic;
using TourAgency.Bll.DTO;

namespace TourAgency.Bll.Services.Interfaces
{
    public interface ITourService : IDispose
    {
        List<TourDTO> GetAllTours();
        List<TourDTO> GetActiveTours();
        List<TourCustomerDTO> GetRegisteredTours();
        List<TourDTO> GetHotAndNewTours();
        TourDTO GetTourById(int id);
     
    }
}
