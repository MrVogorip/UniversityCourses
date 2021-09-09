using System;
using System.Threading.Tasks;
using System.Linq;
using TourAgency.Bll.DTO;
using TourAgency.Bll.Helpers;
using TourAgency.Bll.Services.Interfaces;
using TourAgency.Dal.Entities;
using TourAgency.Dal.UnitOfWork.Interfaces;
using System.Collections.Generic;
using TourAgency.Bll.BusinessModels;
using TourAgency.Bll.Infrastructure;

namespace TourAgency.Bll.Services
{
    public class CustomerService : TourService, ICustomerService
    {
        private readonly IUnitOfWork _dataBase;
        public CustomerService(IUnitOfWork dataBase) : base(dataBase)
        {
            _dataBase = dataBase;
        }
        public List<TourCustomerDTO> GetTourCustomerByCustomerId(string userId)
        {
            var tourCustomer = _dataBase.TourCustomers.GetAll();
            var customerId = _dataBase.Customers.GetCustomerIdByIdentityUserId(userId);
            var customerTour = tourCustomer.Where(u => u.CustomerId == customerId);
            if (customerTour is null)
            {
                throw new ValidationException("Failed to get manager", "null error");
            }
            var tourDto = MappingDTO.MapTourCustomerListDTO(customerTour.ToList());
            return tourDto;
        }
        public void Register(CustomerDTO model)
        {
            var customer = MappingDTO.MapCustomer(model);
            _dataBase.Customers.Register(customer);
            _dataBase.Save();
        }
        /// <summary>
        ///    tour purchase with a decrease in the number of possible orders
        /// </summary>
        public void BuyTour(TourDTO tourDto, string userId, int realNumberOfPeople, int realPrice)
        {
            var customerId = _dataBase.Customers.GetCustomerIdByIdentityUserId(userId);
            var typeOfStatusRegistered = _dataBase.TypeOfStatuses.Get("Registered");
            var tourCustomer = new TourCustomer()
            {
                TourId = tourDto.Id,
                CustomerId = customerId,
                TypeOfStatusId = typeOfStatusRegistered.Id,
                RealNumberOfPeople = realNumberOfPeople,
                RealPrice = realPrice
            };
            var tour = MappingDTO.MapTour(tourDto);
            tour.NumberOfOrders--;
            _dataBase.Tours.UpdateInfo(tour);
            _dataBase.TourCustomers.Create(tourCustomer);
            _dataBase.Save();
        }
        public CustomerDTO GetCustomerByIdentityUserId(string userId)
        {
            var customerId = _dataBase.Customers.GetCustomerIdByIdentityUserId(userId);
            if (customerId == -1)
            {
                return null;
            }
            var customer =  _dataBase.Customers.Get(customerId);
            var customerDto = MappingDTO.MapCustomerDTO(customer);
            return customerDto;
        }
        public void ChangePersonalInformation(CustomerDTO customerDto)
        {
            var customer = _dataBase.Customers.Get(customerDto.Id);
            if (customer != null)
            {
                customer.Name = customerDto.Name;
                customer.Surname = customerDto.Surname;
                _dataBase.Customers.Update(customer);
            }
            _dataBase.Save();
        }
        /// <summary>
        ///    cancellation of the tour with a reduction in discounts for customer
        /// </summary>
        public void CancelTour(TourCustomerDTO tourCustomer)
        {
            _dataBase.TourCustomers.Delete(tourCustomer.Id);
            _dataBase.Save();
            var tourDto = tourCustomer.Tour;
            tourDto.NumberOfOrders++;
            var tour = MappingDTO.MapTour(tourDto);
            _dataBase.Tours.UpdateInfo(tour);
            var customerDto = tourCustomer.Customer;
            customerDto.Discount = Discount.ReduceDiscount(customerDto.Discount);
            var customer = MappingDTO.MapCustomer(customerDto);
            _dataBase.Customers.UpdateInfo(customer);
            _dataBase.Save();
        }
        public void SendFeedback(FeedbackDTO feedbackDTO)
        {
            var feedback = MappingDTO.MapFeedback(feedbackDTO);
            _dataBase.Feedbacks.Create(feedback);
            _dataBase.Save();
        }
    }
}
