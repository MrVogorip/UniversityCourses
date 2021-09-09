using AutoMapper;
using System.Collections.Generic;
using TourAgency.Bll.BusinessModels.ResponseModel;
using TourAgency.Bll.DTO;
using System.Linq;

using TourAgency.Bll.Helpers;
using TourAgency.Bll.Infrastructure;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Dal.Entities;
using TourAgency.Dal.UnitOfWork.Interfaces;
using TourAgency.Bll.BusinessModels;

namespace TourAgency.Bll.Services
{
    public class ManagerService : TourService, IManagerService
    {
        private readonly IUnitOfWork _dataBase;
        public ManagerService(IUnitOfWork dataBase) : base(dataBase)
        {
            _dataBase = dataBase;
        }
        public void RegisterManager(ManagerDTO managerDTO)
        {
            var manager = MappingDTO.MapManager(managerDTO);
            _dataBase.Managers.Register(manager);
            _dataBase.Save();
        }
        public void UpdateTour(TourDTO tourDTO)
        {
            var tour = MappingDTO.MapTour(tourDTO);
            _dataBase.Tours.UpdateInfo(tour);
            _dataBase.Save();
        }
        public void BlockCustomer(int id)
        {
            var customer = _dataBase.Customers.Get(id);
            if (customer != null)
            {
                customer.IsBlock = true;
                _dataBase.Customers.Update(customer);
            }
            else
                throw new ValidationException("Failed to block customer", "null error");
            _dataBase.Save();
        }
        public void UnlockCustomer(int id)
        {
            var customer = _dataBase.Customers.Get(id);
            if (customer != null)
            {
                customer.IsBlock = false;
                _dataBase.Customers.Update(customer);
            }
            else
                throw new ValidationException("Failed to unlock customer", "null error");
            _dataBase.Save();
        }
        /// <summary>
        ///    create a list of options for creating a tour
        /// </summary>
        public ListOptionModel GetListOption()
        {
            var cites = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<City, CityDTO>())).Map<List<CityDTO>>(_dataBase.Cites.GetAll());
            var typeOfHotels = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TypeOfHotel, TypeOfHotelDTO>())).Map<List<TypeOfHotelDTO>>(_dataBase.TypeOfHotels.GetAll());
            var typeOfTours = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TypeOfTour, TypeOfTourDTO>())).Map<List<TypeOfTourDTO>>(_dataBase.TypeOfTours.GetAll());
            var typeOfStatus = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TypeOfStatus, TypeOfStatusDTO>())).Map<List<TypeOfStatusDTO>>(_dataBase.TypeOfStatuses.GetAll());
            var listOptionModel = new ListOptionModel()
            {
                Cities = cites,
                TypeOfHotels = typeOfHotels,
                TypeOfTours = typeOfTours,
                TypeOfStatuses = typeOfStatus
            };
            return listOptionModel;
        }
        public void UpdateTourCustomer(TourCustomerDTO tourCustomerDTO)
        {
            var typeOfStatusPaid = _dataBase.TypeOfStatuses.Get("Paid");
            if (tourCustomerDTO.TypeOfStatusId == typeOfStatusPaid.Id)
            {
                var customerDto = tourCustomerDTO.Customer;
                var customer = MappingDTO.MapCustomer(customerDto);
                customer.Discount = Discount.AddDiscount(customer.Discount, customer.StepDiscount, customer.MaxDiscount);
                _dataBase.Customers.UpdateInfo(customer);
                _dataBase.Save();
            }
            _dataBase.TourCustomers.SetStatus(tourCustomerDTO.Id, tourCustomerDTO.TypeOfStatusId);
            _dataBase.Save();
        }
        public List<CustomerDTO> GetAllCustomers()
        {
            var customers = _dataBase.Customers.GetAll();
            var customersDto = MappingDTO.MapCustomerListDTO(customers.ToList());
            return customersDto;
        }
        public CustomerDTO GetCustomerById(int id)
        {
            var customer = _dataBase.Customers.Get(id);
            if (customer is null)
            {
                throw new ValidationException("Failed to get customer", "null error");
            }
            var customerDto = MappingDTO.MapCustomerDTO(customer);
            return customerDto;
        }
        public void ChangeDiscountCustomer(CustomerDTO customerDto)
        {
            var customer = _dataBase.Customers.Get(customerDto.Id);
            if (customer != null)
            {
                customer.MaxDiscount = customerDto.MaxDiscount;
                customer.StepDiscount = customerDto.StepDiscount;
                _dataBase.Customers.Update(customer);
            }
            else
                throw new ValidationException("Failed to change discount customer", "null error");
            _dataBase.Save();
        }
        public ManagerDTO GetManagerByIdentityUserId(string userId)
        {
            var managerId = _dataBase.Managers.GetCManagerIdByIdentityUserId(userId);
            if (managerId == -1)
            {
                return null;
            }
            var manager = _dataBase.Managers.Get(managerId);
            var managerDto = MappingDTO.MapManagerDTO(manager);
            return managerDto;
        }
        public List<FeedbackDTO> GetActiveFeedbacks()
        {
            var feedbacks = _dataBase.Feedbacks.GetAll().Where(f => f.IsRead == false).ToList();
            var feedbacksDto = MappingDTO.MapFeedbackListDTO(feedbacks);
            return feedbacksDto;
        }
        public void ReadFeedback(int id)
        {
            var feedback = _dataBase.Feedbacks.Get(id);
            if (feedback is null)
            {
                throw new ValidationException("Failed to get feedback", "null error");
            }
            feedback.IsRead = true;
            _dataBase.Feedbacks.Update(feedback);
            _dataBase.Save();
        }
    }
}
