using System;
using TourAgency.Dal.Repositories;

namespace TourAgency.Dal.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        AdministratorRepository Administrators { get; }
        CityRepository Cites { get; }
        CustomerRepository Customers { get; }
        ManagerRepository Managers { get; }
        TourRepository Tours { get; }
        TypeOfHotelRepository TypeOfHotels { get; }
        TypeOfStatusRepository TypeOfStatuses { get; }
        TypeOfTourRepository TypeOfTours { get; }
        TourCustomersRepository TourCustomers { get; }
        FeedbackRepository Feedbacks { get; }
        void Save();
    }
}
