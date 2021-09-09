using System;
using System.Collections.Generic;
using TourAgency.Bll.DTO;
using TourAgency.Bll.Helpers;
using System.Linq;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Dal.Entities;
using TourAgency.Dal.UnitOfWork.Interfaces;

namespace TourAgency.Bll.Services
{
    public class TourService : ITourService
    {
        private readonly IUnitOfWork _dataBase;
        public TourService(IUnitOfWork dataBase)
        {
            _dataBase = dataBase;
        }
        public void Dispose()
        {
            _dataBase.Dispose();
        }
        public List<TourDTO> GetAllTours()
        {
            var tours = _dataBase.Tours.GetAll();
            var toursDto = MappingDTO.MapTourListDTO(tours.ToList());
            return toursDto;
        }
        public List<TourDTO> GetActiveTours()
        {
            var tours = _dataBase.Tours.GetAll();
            var activeTours = new List<TourDTO>();
            foreach (var item in tours)
            {
                if (item.NumberOfOrders > 0 && !item.IsDelete)
                {
                    var tourDto = MappingDTO.MapTourDTO(item);
                    activeTours.Add(tourDto);
                }
            }
            return activeTours;
        }
        public TourDTO GetTourById(int id)
        {
            var tour = _dataBase.Tours.Get(id);
            if (tour is null)
                return null;
            var tourDto = MappingDTO.MapTourDTO(tour);
            return tourDto;
        }
        public List<TourCustomerDTO> GetRegisteredTours()
        {
            var tours = _dataBase.TourCustomers.GetAll();
            var typeOfStatusRegistered = _dataBase.TypeOfStatuses.Get("Registered");
            var registeredTours = new List<TourCustomerDTO>();
            foreach (var item in tours)
            {
                if (item.TypeOfStatusId == typeOfStatusRegistered.Id)
                {
                    var tourDto = MappingDTO.MapTourCustomerDTO(item);
                    registeredTours.Add(tourDto);
                }
            }
            return registeredTours;
        }
        public List<TourDTO> GetHotAndNewTours()
        {
            var tours = _dataBase.Tours.GetAll();
            var hotTours = new List<TourDTO>();
            foreach (var item in tours)
            {
                if (item.IsHot && item.NumberOfOrders > 0 && !item.IsDelete)
                {
                    var tourDto = MappingDTO.MapTourDTO(item);
                    hotTours.Add(tourDto);
                }
            }
            return hotTours;
        }
    }
}
